using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Diagnostics;

namespace Tap5050Buyer
{
    public partial class RegistrationPage : ContentPage
    {
        private AccountInfoViewModel _viewModel;

        public List<string> c_contactMethods = new List<string>() { "Do not contact me", "SMS", "Email" };

        public RegistrationPage(bool isUpdate, Page parent, UserAccount userAccount = null)
        {
            InitializeComponent();
            if (!isUpdate)
            {
                Title = "Registration";
            }
            else
            {
                Title = "Update Information";
            }
            _tableView.Intent = TableIntent.Menu;

            if (userAccount == null)
            {
                _viewModel = new AccountInfoViewModel();
            }
            else
            {
                _viewModel = new AccountInfoViewModel(userAccount);
            }
            this.BindingContext = _viewModel;

            #region Birthday
            var birthdayCell = new ViewCell();
            _birthdaySection.Add(birthdayCell);

            var birthdayLayout = new StackLayout
            {
                Padding = new Thickness(10, 0, 10, 0),
            };
            birthdayCell.View = birthdayLayout;

            var datePicker = new DatePicker
            {
                Format = "MMM d yyyy",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };
            birthdayLayout.Children.Add(datePicker);
            datePicker.SetBinding(DatePicker.DateProperty, "UserAccount.Birthday", BindingMode.TwoWay);
            #endregion

            #region Province 
            var provinceCellLayout = new StackLayout
            {
                Padding = new Thickness(10, 0, 10, 0),
            };
            _provinceCell.View = provinceCellLayout;

            var provincePicker = new Picker
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };
            provinceCellLayout.Children.Add(provincePicker);

            var firstCountry = DatabaseManager.DbConnection.Table<Country>().First();
            var provinces = DatabaseManager.DbConnection.Table<Province>().Where(x => x.CountryCode == firstCountry.CountryCode).ToList();
            foreach (var province in provinces)
            {
                provincePicker.Items.Add(province.ProvinceName);
            }
            provincePicker.SetBinding(Picker.SelectedIndexProperty, new Binding("UserAccount.Province", BindingMode.TwoWay, new PickerProvinceToIndexConverter(), provinces));
            #endregion

            #region Country
            var countryCellLayout = new StackLayout
            {
                Padding = new Thickness(10, 0, 10, 0),
            };
            _countryCell.View = countryCellLayout;

            var countryPicker = new Picker
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };
            countryCellLayout.Children.Add(countryPicker);

            var countries = DatabaseManager.DbConnection.Table<Country>().ToList();
            foreach (var country in countries)
            {
                countryPicker.Items.Add(country.CountryName);
            }
            countryPicker.SetBinding(Picker.SelectedIndexProperty, new Binding("UserAccount.Country", BindingMode.TwoWay, new PickerCountryToIndexConverter(), countries));

            countryPicker.SelectedIndexChanged += (sender, e) =>
            {
                provincePicker.Items.Clear();

                var countryCode = countries[countryPicker.SelectedIndex].CountryCode;
                var newProvinces = DatabaseManager.DbConnection.Table<Province>().Where(x => x.CountryCode == countryCode).ToList();

                // If there is no province, use N/A
                if ((newProvinces == null) || (newProvinces.Count == 0))
                {
                    newProvinces = new List<Province>
                    {
                        new Province
                        {
                            ProvinceName = "N/A",
                        }
                    };
                }
                
                foreach (var province in newProvinces)
                {
                    provincePicker.Items.Add(province.ProvinceName);
                }
                provincePicker.SetBinding(Picker.SelectedIndexProperty, new Binding("UserAccount.Province", BindingMode.TwoWay, new PickerProvinceToIndexConverter(), newProvinces));
            };
            #endregion

            #region Raffle Result
            var raffleResultsViewCell = new ViewCell();
            _raffleResultsSection.Add(raffleResultsViewCell);

            var raffleResultsLayout = new StackLayout
            {
                Padding = new Thickness(10, 0, 10, 0),
            };
            raffleResultsViewCell.View = raffleResultsLayout;

            var raffleResultsPicker = new Picker
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };
            raffleResultsLayout.Children.Add(raffleResultsPicker);

            foreach (var item in c_contactMethods)
            {
                raffleResultsPicker.Items.Add(item);
            }
            raffleResultsPicker.SetBinding(Picker.SelectedIndexProperty, new Binding("UserAccount.PreferedContactMethod", BindingMode.TwoWay, new PickerContactMethodsConverter()));
            #endregion

            #region Charity Message
            var charityMessagesViewCell = new ViewCell();
            _charityMessagesSection.Add(charityMessagesViewCell);

            var charityMessagesLayout = new StackLayout
            {
                Padding = new Thickness(10, 0, 10, 0),
            };
            charityMessagesViewCell.View = charityMessagesLayout;

            var charityMessagesPicker = new Picker
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };
            charityMessagesLayout.Children.Add(charityMessagesPicker);

            foreach (var item in c_contactMethods)
            {
                charityMessagesPicker.Items.Add(item);
            }
            charityMessagesPicker.SetBinding(Picker.SelectedIndexProperty, new Binding("UserAccount.PreferedContactMethodCharity", BindingMode.TwoWay, new PickerContactMethodsConverter()));
            #endregion

            if (!isUpdate)
            {
                var createAccountButtonViewCell = new ViewCell();
                _createAccountButtonSection.Add(createAccountButtonViewCell);

                var createAccountButtonLayout = new StackLayout
                {
                    Padding = new Thickness(5, 0, 5, 0),
                    BackgroundColor = Color.Accent,
                };
                createAccountButtonViewCell.View = createAccountButtonLayout;

                var createAccountButton = new Button
                {
                    Text = "Create Account",
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    TextColor = Color.White,
                };
                createAccountButtonLayout.Children.Add(createAccountButton);

                createAccountButton.Clicked += async (sender, e) =>
                {
                    var result = await _viewModel.CreateAccount();
                    if (result.Item1)
                    {
                        this.Navigation.PopAsync();
                        var answer = await DisplayAlert("Register Phone Number", "Send registration code to your phone now?", "Later", "Yes");
                        if (!answer) // use !answer because the negative choice has a bigger font
                        {
                            parent.Navigation.PushAsync(new VerifyPhonePage(_viewModel.UserAccount.Email, _viewModel.UserAccount.Phone, _viewModel.UserAccount.CountryCode)); //Country code is updated after calling _viewModel.CreateAccount()
                        }
                    }
                    else
                    {
                        DisplayAlert("Server request failed", "", "OK");
                    }
                };
            }
            else
            {
                // Edit Account Page

                Count = 0; // For test
                Debug.WriteLine(Count);

                _toolbarItemIsEnabled = true;

                _onDoneButtonClicked = new Command(async () =>
                    {
                        if (_toolbarItemIsEnabled)
                        {
                            Count++;
                            Debug.WriteLine("click: " + Count);

                            if (_viewModel.InfoHasNotChanged())
                            {
                                DisplayAlert("Warning", "Your information has not changed.", "Retry");
                            }
                            else
                            {
                                _toolbarItemIsEnabled = false; // Disable the button while await
                                var result = await _viewModel.UpdateAccountInfo();
                                _toolbarItemIsEnabled = true;
                                if (result.Item1)
                                {
//                                this.Navigation.InsertPageBefore(new AccountInfoPage(), parent);
                                    this.Navigation.PopAsync(false);
                                    this.Navigation.PopAsync(false);
                                    this.Navigation.PushAsync(new AccountInfoPage(), false);
                                }
                                else
                                {
                                    DisplayAlert("Error", result.Item2, "Retry");    
                                } 
                            }
                        }
                    });
                
                var toolbarItem = new ToolbarItem
                {
                    Text = "Done",
                    Command = _onDoneButtonClicked,
                };
                this.ToolbarItems.Add(toolbarItem);
            }
        }

        private Command _onDoneButtonClicked;
        private bool _toolbarItemIsEnabled;

        // Tried to disable toolbar item following the bellow thread without success
        // http://stackoverflow.com/questions/27803038/disable-toolbaritem-xamarin-forms
        //        private bool _toolbarItemIsEnabled;
        //
        //        public bool ToolbarItemIsEnabled
        //        {
        //            get
        //            {
        //                return _toolbarItemIsEnabled;
        //            }
        //            set
        //            {
        //                if (_toolbarItemIsEnabled != value)
        //                {
        //                    _toolbarItemIsEnabled = value;
        //                    _onDoneButtonClicked.ChangeCanExecute();
        //                    OnPropertyChanged();
        //                }
        //            }
        //        }

        public int Count { get; set; }
    }
}

