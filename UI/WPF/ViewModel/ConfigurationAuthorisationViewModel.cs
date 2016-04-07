﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using DevelopmentInProgress.AuthorisationManager.WPF.Model;
using DevelopmentInProgress.DipSecure;
using DevelopmentInProgress.Origin.Context;
using DevelopmentInProgress.Origin.ViewModel;
using DevelopmentInProgress.WPFControls.Command;

namespace DevelopmentInProgress.AuthorisationManager.WPF.ViewModel
{
    public class ConfigurationAuthorisationViewModel : DocumentViewModel
    {
        private EntityBase selectedItem;

        private readonly AuthorisationManagerService serviceManager;

        public ConfigurationAuthorisationViewModel(ViewModelContext viewModelContext, AuthorisationManagerService serviceManager)
            : base(viewModelContext)
        {
            this.serviceManager = serviceManager;

            NewUserCommand = new WpfCommand(OnNewUser);
            NewRoleCommand = new WpfCommand(OnNewRole);
            NewActivityCommand = new WpfCommand(OnNewActivity);
            SaveCommand = new WpfCommand(OnEntitySave);
            DeleteCommand = new WpfCommand(OnEntityDelete);
            AddItemCommand = new WpfCommand(OnAddItem);
            RemoveItemCommand = new WpfCommand(OnRemoveItem);
            SelectItemCommand = new WpfCommand(OnSelectItem);
            DragDropCommand = new WpfCommand(OnDragDrop);
        }

        public ICommand NewUserCommand { get; set; }

        public ICommand NewRoleCommand { get; set; }

        public ICommand NewActivityCommand { get; set; }

        public ICommand SaveCommand { get; set; }

        public ICommand DeleteCommand { get; set; }

        public ICommand AddItemCommand { get; set; }

        public ICommand RemoveItemCommand { get; set; }

        public ICommand SelectItemCommand { get; set; }

        public ICommand DragDropCommand { get; set; }

        public ObservableCollection<ActivityNode> Activities { get; set; }

        public ObservableCollection<RoleNode> Roles { get; set; }

        public ObservableCollection<UserNode> Users { get; set; }

        public EntityBase SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        protected override ProcessAsyncResult OnPublishedAsync(object data)
        {
            return base.OnPublishedAsync(data);
        }

        protected override void OnPublishedAsyncCompleted(ProcessAsyncResult processAsyncResult)
        {
            base.OnPublishedAsyncCompleted(processAsyncResult);

            Activities = new ObservableCollection<ActivityNode>(serviceManager.GetActivityNodes());
            Roles = new ObservableCollection<RoleNode>(serviceManager.GetRoleNodes());
            Users = new ObservableCollection<UserNode>(serviceManager.GetUserNodes());
        }

        protected override ProcessAsyncResult SaveDocumentAsync()
        {
            return base.SaveDocumentAsync();
        }

        private void OnNewUser(object param)
        {
            SelectedItem = new UserNode(new UserAuthorisation());
        }

        private void OnNewRole(object param)
        {
            SelectedItem = new RoleNode(new Role());
        }

        private void OnNewActivity(object param)
        {
            SelectedItem = new ActivityNode(new Activity());
        }

        private void OnEntitySave(object param)
        {
            if (param == null)
            {
                return;
            }

            var activityNode = param as ActivityNode;
            if (activityNode != null)
            {
                SaveActivity(activityNode);
                return;
            }

            var roleNode = param as RoleNode;
            if (roleNode != null)
            {
                SaveRole(roleNode);
                return;
            }

            var userNode = param as UserNode;
            if (userNode != null)
            {
                SaveUser(userNode);
            }
        }

        private void OnEntityDelete(object param)
        {

        }

        private void OnAddItem(object param)
        {

        }

        private void OnRemoveItem(object param)
        {

        }

        private void OnSelectItem(object param)
        {

        }
        
        private void OnDragDrop(object param)
        {

        }

        private void SaveActivity(ActivityNode activityNode)
        {
            var newActivity = activityNode.Id.Equals(0);

            var savedActivity = serviceManager.SaveActivity(activityNode);

            if (savedActivity != null)
            {
                if (newActivity)
                {
                    Activities.Add(savedActivity);
                }
                else
                {
                    var updatedActivity = Activities.First(a => a.Id.Equals(savedActivity.Id));
                    updatedActivity.Text = savedActivity.Text;
                    updatedActivity.Code = savedActivity.Code;
                    updatedActivity.Description = savedActivity.Description;
                }
            }
        }

        private void SaveRole(RoleNode roleNode)
        {

        }

        private void SaveUser(UserNode userNode)
        {

        }
    }
}
