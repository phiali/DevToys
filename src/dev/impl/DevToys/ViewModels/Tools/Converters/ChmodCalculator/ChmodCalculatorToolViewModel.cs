#nullable enable

using System;
using System.Collections.Generic;
using System.Composition;
using System.Diagnostics;
using DevToys.Api.Core;
using DevToys.Api.Core.Settings;
using DevToys.Api.Tools;
using DevToys.Models;
using DevToys.Views.Tools.ChmodCalculator;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace DevToys.ViewModels.Tools.ChmodCalculator
{
    [Export(typeof(ChmodCalculatorToolViewModel))]
    public sealed class ChmodCalculatorToolViewModel : ObservableRecipient, IToolViewModel
    {
        private bool _updatingProgrammatically;
        private bool _isOwnerRead;
        private bool _isOwnerWrite;
        private bool _isOwnerExecute;
        private bool _isGroupRead;
        private bool _isGroupWrite;
        private bool _isGroupExecute;
        private bool _isOtherRead;
        private bool _isOtherWrite;
        private bool _isOtherExecute;
        private String _textPermissions;
        private int _octalRepresentation;
        private String _textOctalRepresentation;
        private double _doubleOctalRepresentation;

        internal bool IsOwnerRead
        {
            get => _isOwnerRead;
            set
            {
                if (_isOwnerRead != value)
                {
                    _isOwnerRead = value;
                    OnPropertyChanged(nameof(IsOwnerRead));
                    if (!_updatingProgrammatically)
                    {
                        applyChangesFromCheckboxes();
                    }
                }
            }
        }

        internal bool IsOwnerWrite
        {
            get => _isOwnerWrite;
            set
            {
                if (_isOwnerWrite != value)
                {
                    _isOwnerWrite = value;
                    OnPropertyChanged(nameof(IsOwnerWrite));
                    if (!_updatingProgrammatically)
                    {
                        applyChangesFromCheckboxes();
                    }
                }
            }
        }

        internal bool IsOwnerExecute
        {
            get => _isOwnerExecute;
            set
            {
                if (_isOwnerExecute != value)
                {
                    _isOwnerExecute = value;
                    OnPropertyChanged(nameof(IsOwnerExecute));
                    if (!_updatingProgrammatically)
                    {
                        applyChangesFromCheckboxes();
                    }
                }
            }
        }

        internal bool IsGroupRead
        {
            get => _isGroupRead;
            set
            { 
                if (_isGroupRead != value)
                {
                    _isGroupRead = value;
                    OnPropertyChanged(nameof(IsGroupRead));
                    if (!_updatingProgrammatically)
                    {
                        applyChangesFromCheckboxes();
                    }
                }
            }
        }

        internal bool IsGroupWrite
        {
            get => _isGroupWrite;
            set
            {
                if (_isGroupWrite != value)
                {
                    _isGroupWrite = value;
                    OnPropertyChanged(nameof(IsGroupWrite));
                    if (!_updatingProgrammatically)
                    {
                        applyChangesFromCheckboxes();
                    }
                }
            }
        }

        internal bool IsGroupExecute
        {
            get => _isGroupExecute;
            set
            {
                if (_isGroupExecute != value)
                {
                    _isGroupExecute = value;
                    OnPropertyChanged(nameof(IsGroupExecute));
                    if (!_updatingProgrammatically)
                    {
                        applyChangesFromCheckboxes();
                    }
                }
            }
        }

        internal bool IsOtherRead
        {
            get => _isOtherRead;
            set
            {
                if (_isOtherRead != value)
                {
                    _isOtherRead = value;
                    OnPropertyChanged(nameof(IsOtherRead));
                    if (!_updatingProgrammatically)
                    {
                        applyChangesFromCheckboxes();
                    }
                }
            }
        }

        internal bool IsOtherWrite
        {
            get => _isOtherWrite;
            set
            {
                if (_isOtherWrite != value)
                {
                    _isOtherWrite = value;
                    OnPropertyChanged(nameof(IsOtherWrite));
                    if (!_updatingProgrammatically)
                    {
                        applyChangesFromCheckboxes();
                    }
                }
            }
        }

        internal bool IsOtherExecute
        {
            get => _isOtherExecute;
            set
            {
                if (_isOtherExecute != value)
                {
                    _isOtherExecute = value;
                    OnPropertyChanged(nameof(IsOtherExecute));
                    if (!_updatingProgrammatically)
                    {
                        applyChangesFromCheckboxes();
                    }
                }
            }
        }

        internal String TextPermissions
        {
            get => _textPermissions;
            set
            {
                if (_textPermissions != value)
                {
                    _textPermissions = value;
                    OnPropertyChanged(nameof(TextPermissions));
                }
            }
        }

        internal int OctalRepresentation
        {
            get => _octalRepresentation;
            set
            {
                if (_octalRepresentation != value)
                {
                    _octalRepresentation = value;
                    OnPropertyChanged(nameof(_octalRepresentation));
                }
            }
        }

        internal String TextOctalRepresentation
        {
            get => _textOctalRepresentation;
            set
            {
                if (_textOctalRepresentation != value)
                {
                    _textOctalRepresentation = value;
                    OnPropertyChanged(nameof(TextOctalRepresentation));

                    if (!_updatingProgrammatically)
                    {
                        applyChangesFromOctal();
                    }
                }
            }
        }

        internal double DoubleOctalRepresentation
        {
            get => _doubleOctalRepresentation;
            set
            {
                if (_doubleOctalRepresentation != value)
                {
                    _doubleOctalRepresentation = value;
                    OnPropertyChanged(nameof(TextOctalRepresentation));
                }
            }
        }

        public Type View { get; } = typeof(ChmodCalculatorToolPage);

        internal ChmodCalculatorStrings Strings => LanguageManager.Instance.ChmodCalculator;

        internal ISettingsProvider SettingsProvider { get; }

        [ImportingConstructor]
        public ChmodCalculatorToolViewModel(ISettingsProvider settingsProvider, IMarketingService marketingService)
        {
            SettingsProvider = settingsProvider;
            //_marketingService = marketingService;
            _textPermissions = "";
            _textOctalRepresentation = "";
        }

        private int getOctalPermissions()
        {
            int ownerValue = 0;
            int groupValue = 0;
            int otherValue = 0;

            if (IsOwnerRead)
            {
                ownerValue += 4;
            }

            if (IsOwnerWrite)
            {
                ownerValue += 2;
            }

            if (IsOwnerExecute)
            {
                ownerValue += 1;
            }

            if (IsGroupRead)
            {
                groupValue += 4;
            }

            if (IsGroupWrite)
            {
                groupValue += 2;
            }

            if (IsGroupExecute)
            {
                groupValue += 1;
            }

            if (IsOtherRead)
            {
                otherValue += 4;
            }

            if (IsOtherWrite)
            {
                otherValue += 2;
            }

            if (IsOtherExecute)
            {
                otherValue += 1;
            }

            string a = ownerValue.ToString() + groupValue.ToString() + otherValue.ToString();

            return int.Parse(a);
        }

        private void applyChangesFromCheckboxes()
        {
            int octalPermissions = getOctalPermissions();
            string textPermissions = getTextPermissionsFromOctal(octalPermissions.ToString());

            _updatingProgrammatically = true;
            TextPermissions = textPermissions;
            TextOctalRepresentation = octalPermissions.ToString();
            _updatingProgrammatically = false;
        }

        private void applyChangesFromOctal()
        {
            string textPermissions = getTextPermissionsFromOctal(TextOctalRepresentation);

            _updatingProgrammatically = true;
            // Update the checkboxes
            {
                IsOwnerRead = false;
                IsOwnerWrite = false;
                IsOwnerExecute = false;

                IsGroupRead = false;
                IsGroupWrite = false;
                IsGroupExecute = false;

                IsOtherRead = false;
                IsOtherWrite = false;
                IsOtherExecute = false;

                if (_textOctalRepresentation != null)
                {
                    for (int position = 0; position < _textOctalRepresentation.Length; position++)
                    {
                        int value = int.Parse(_textOctalRepresentation[position].ToString());

                        switch (value)
                        {
                            case 0:

                                break;

                            case 1:
                                if (position == 0)
                                {
                                    IsOwnerExecute = true;
                                } else if (position == 1)
                                {
                                    IsGroupExecute = true;
                                } else if (position == 2)
                                {
                                    IsOtherExecute= true;
                                }
                                break;

                            case 2:
                                if (position == 0)
                                {
                                    IsOwnerWrite = true;
                                }
                                else if (position == 1)
                                {
                                    IsGroupWrite = true;
                                }
                                else if (position == 2)
                                {
                                    IsOtherWrite = true;
                                }
                                break;

                            case 3:
                                if (position == 0)
                                {
                                    IsOwnerWrite = true;
                                    IsOwnerExecute = true;
                                }
                                else if (position == 1)
                                {
                                    IsGroupWrite = true;
                                    IsGroupExecute = true;
                                }
                                else if (position == 2)
                                {
                                    IsOtherWrite = true;
                                    IsOtherExecute = true;
                                }
                                break;

                            case 4:
                                if (position == 0)
                                {
                                    IsOwnerRead = true;
                                }
                                else if (position == 1)
                                {
                                    IsGroupRead = true;
                                }
                                else if (position == 2)
                                {
                                    IsOtherRead = true;
                                }
                                break;

                            case 5:
                                if (position == 0)
                                {
                                    IsOwnerRead = true;
                                    IsOwnerExecute = true;
                                }
                                else if (position == 1)
                                {
                                    IsGroupRead = true;
                                    IsGroupExecute = true;
                                }
                                else if (position == 2)
                                {
                                    IsOtherRead = true;
                                    IsOtherExecute = true;
                                }
                                break;

                            case 6:
                                if (position == 0)
                                {
                                    IsOwnerRead = true;
                                    IsOwnerWrite = true;
                                }
                                else if (position == 1)
                                {
                                    IsGroupRead = true;
                                    IsGroupWrite = true;
                                }
                                else if (position == 2)
                                {
                                    IsOtherRead = true;
                                    IsOtherWrite = true;
                                }
                                break;

                            case 7:
                                if (position == 0)
                                {
                                    IsOwnerRead = true;
                                    IsOwnerWrite = true;
                                    IsOwnerExecute = true;
                                }
                                else if (position == 1)
                                {
                                    IsGroupRead = true;
                                    IsGroupWrite = true;
                                    IsGroupExecute = true;
                                }
                                else if (position == 2)
                                {
                                    IsOtherRead = true;
                                    IsOtherWrite = true;
                                    IsOtherExecute = true;
                                }
                                break;
                        }
                    }
                }
            }

            TextPermissions = textPermissions;

            _updatingProgrammatically = false;
        }

        private string getTextPermissionsFromOctal(string octal)
        {
            string result = "";

            if (octal != null)
            {
                for (int i = 0; i < octal.Length; i++)
                {
                    int value = int.Parse(octal[i].ToString());

                    switch (value)
                    {
                        case 0:
                            result += "---";
                            break;
                        case 1:
                            result += "--x";
                            break;
                        case 2:
                            result += "-w-";
                            break;
                        case 3:
                            result += "-wx";
                            break;
                        case 4:
                            result += "r--";
                            break;
                        case 5:
                            result += "r-x";
                            break;
                        case 6:
                            result += "rw-";
                            break;
                        case 7:
                            result += "rwx";
                            break;
                    }
                }
            }

            return result;
        }
    }
}
