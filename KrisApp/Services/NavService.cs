﻿using KrisApp.Common.Extensions;
using KrisApp.DataModel.Dictionaries;
using KrisApp.DataModel.Users;
using KrisApp.Models.Nav;
using System.Collections.Generic;
using System.Linq;
using System;

namespace KrisApp.Services
{
    public class NavService : AbstractService
    {
        private readonly ArticleService _articleSrv;
        private readonly User _user;

        public NavService(KrisLogger log) : base(log)
        {
            _articleSrv = new ArticleService(log);
            _user = SessionService.GetFromSession<User>(SessionService.SessionItem.User);
        }

        /// <summary>
        /// Zwraca model zawierający elementy Main Menu
        /// </summary>
        internal MenuModel PrepareMenuModel(string controller)
        {
            MenuModel model = new MenuModel() { MenuItems = new List<MenuItemModel>() };
            
            model.Login = _user?.Login;

            MenuItemModel articleMenu = PrepareArticleMenu();
            model.MenuItems.Add(articleMenu);

            MenuItemModel rekruMenu = PrepareRekruMenu();
            model.MenuItems.Add(rekruMenu);

            MenuItemModel aboutMenu = PrepareAboutMenu();
            model.MenuItems.Add(aboutMenu);


            if (_user?.Type.Code == UserType.UserTypeCodes.ADM.ToString())
            {
                model.MenuItems.Add(PrepareAdminItems());
            }

            foreach (MenuItemModel item in model.MenuItems)
            {
                if (item.Controller == controller)
                {
                    item.Active = true;
                    break;
                }
            }

            return model;

        }

        /// <summary>
        /// Zwraca element menu dotyczący artykułów
        /// </summary>
        private MenuItemModel PrepareArticleMenu()
        {
            IEnumerable<ArticleType> articleTypes = _articleSrv.GetArticleTypes().Where(x => x.IsMain == false);

            MenuItemModel m = new MenuItemModel()
            {
                Controller = "XXX",
                Text = "Artykuły",
                SubMenu = new List<MenuItemModel>()
            };

            #region Dodanie artykułu widoczne dla Moderatora i Administratora

            if (_user?.Type?.Code != null && 
                _user.Type.Code.In(UserType.UserTypeCodes.MOD.ToString(), UserType.UserTypeCodes.ADM.ToString()))
            {
                MenuItemModel addArticleItem = new MenuItemModel()
                {
                     Action = "Create",
                     Controller = "Article",
                      Text = "Dodaj artykuł"
                };
                m.SubMenu.Add(addArticleItem);
            }

            #endregion

            #region Typy artykułów

            foreach (ArticleType at in articleTypes)
            {
                MenuItemModel item = new MenuItemModel()
                {
                    Action = at.Code,
                    Controller = "Article",
                    Text = at.Name
                };

                m.SubMenu.Add(item);
            }

            #endregion

            MenuItemModel allArticlesItem = new MenuItemModel()
            {
                Action = "List",
                Controller = "Article",
                Text = "Wszystkie artykuły"
            };
            m.SubMenu.Add(allArticlesItem);

            return m;
        }

        /// <summary>
        /// Zwraca element menu dotyczący rekrutacji
        /// </summary>
        private MenuItemModel PrepareRekruMenu()
        {
            return new MenuItemModel()
            {
                Action = "Index",
                Controller = "Rekru",
                Text = "Rekrutacja"
            };
        }

        /// <summary>
        /// Zwraca element menu dotyczący rekrutacji
        /// </summary>
        private MenuItemModel PrepareAboutMenu()
        {
            return new MenuItemModel()
            {
                Action = "XXX",
                Controller = "Me",
                Text = "O mnie",
                SubMenu = new List<MenuItemModel>()
                {
                    new MenuItemModel { Action = "About", Controller = "Me", Text = "O mnie" },
                    new MenuItemModel { Action = "Website", Controller = "Me", Text = "O stronie" },
                    new MenuItemModel { Action = "Contact", Controller = "Me", Text = "Kontakt" },
                }
            };
        }

        /// <summary>
        /// Zwraca elementy do menu Admina
        /// </summary>
        private MenuItemModel PrepareAdminItems()
        {
            return new MenuItemModel()
            {
                Action = "XXX",
                Controller = "XXX",
                Text = "Admin",
                SubMenu = new List<MenuItemModel>()
                {
                    new MenuItemModel { Action = "Pending", Controller = "User", Text = "Konta oczekujące" },
                    new MenuItemModel { Action = "Logs", Controller = "Admin", Text = "Logi" },
                    new MenuItemModel { Action = "ContactMessages", Controller = "Admin", Text = "Przesłane wiadomości" },
                    new MenuItemModel { Action = "Articles", Controller = "Admin", Text = "Artykuły" },
                }
            };
        }
    }
}