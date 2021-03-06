﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConceptMatrix.ViewModel
{
    public class MenuModel
    {
        #region Menu item
        public enum MenuCategory
        {
            Hello,
            Resource,
            Data,
            Equipment,
            Camera,
            Zone,
            GPoseFilter,
            Matrix,
            ModelData,
            Properties,
            EquipmentProp,
            Civic,
            Debug1,
        }

        public class MenuItemModel : INotifyPropertyChanged
        {
            // 大分类
            private MenuCategory _category;
            public MenuCategory Category { get { return _category; } set { onChange(ref _category, value); } }

            // 是否选中
            private bool _isMarked;
            public bool IsMarked { get { return _isMarked; } set { onChange(ref _isMarked, value); } }

            // 左侧的标题文字
            private string _contentText;
            public string ContentText { get { return _contentText; } set { onChange(ref _contentText, value); } }

            // 右侧的气泡文字
            private string _bubbleText;
            public string BubbleText { get { return _bubbleText; } set { onChange(ref _bubbleText, value); } }

            // 实际页面的model
            private object _pageModel;
            public object PageModel { get { return _pageModel; } set { onChange(ref _pageModel, value); } }

            #region On change event

            public event PropertyChangedEventHandler PropertyChanged;

            private bool onChange<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
            {
                if (object.Equals(storage, value))
                {
                    return false;
                }
                storage = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
                }
                return true;
            }

            #endregion
        }

        #endregion

        #region Singleton

        // singleton instance
        private static MenuModel _instance = new MenuModel();

        // get singleton instance
        public static MenuModel Instance
        {
            get
            {
                return _instance;
            }
        }

        #endregion

        #region Menu list

        private MenuModel()
        {
            PlayerList = new ObservableCollection<MenuItemModel>();
            CityList = new ObservableCollection<MenuItemModel>();
            ArmyList = new ObservableCollection<MenuItemModel>();
            ResearchList = new ObservableCollection<MenuItemModel>();
            DebugList = new ObservableCollection<MenuItemModel>();
        }

        public void ClearAll()
        {
            PlayerList.Clear();
            CityList.Clear();
            ArmyList.Clear();
            ResearchList.Clear();
            DebugList.Clear();
        }

        // 当menu被选中时
        public void OnMenuSelected(MenuItemModel selected)
        {
            // 把当前菜单标记为选中
            selected.IsMarked = true;

            // 其余元素标记为未选中
            Action<MenuItemModel> clearMark = (MenuItemModel i) =>
            {
                if (i != selected)
                {
                    i.IsMarked = false;
                }
            };
            foreach (var i in PlayerList)
            {
                clearMark(i);
            }
            foreach (var i in CityList)
            {
                clearMark(i);
            }
            foreach (var i in ArmyList)
            {
                clearMark(i);
            }
            foreach (var i in ResearchList)
            {
                clearMark(i);
            }
            foreach (var i in DebugList)
            {
                clearMark(i);
            }

            // 切换页面
            SwitchPage(selected.Category, selected.PageModel);
        }

        // 以下是各个菜单列表的内容
        public ObservableCollection<MenuItemModel> PlayerList { get; private set; }
        public ObservableCollection<MenuItemModel> CityList { get; private set; }
        public ObservableCollection<MenuItemModel> ArmyList { get; private set; }
        public ObservableCollection<MenuItemModel> ResearchList { get; private set; }
        public ObservableCollection<MenuItemModel> DebugList { get; private set; }

        #endregion

        #region Switch page

        public class SwitchPageEventArgs : EventArgs
        {
            public MenuCategory Category;
            public object DataContext;
        }

        public event EventHandler<SwitchPageEventArgs> SwitchPageRequired;

        public void SwitchPage(MenuCategory category, object dataContext)
        {
            if (SwitchPageRequired != null)
            {
                SwitchPageRequired.Invoke(this, new SwitchPageEventArgs()
                {
                    Category = category,
                    DataContext = dataContext,
                });
            }
        //    Console.WriteLine(category);
       //     Console.WriteLine(dataContext);
        }

        public void ShowMessage(string message)
        {
            HelloPageModel.Instance.PromptText = message;
            SwitchPage(MenuCategory.Hello, HelloPageModel.Instance);
        }

        #endregion
    }
}
