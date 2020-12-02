using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Text.RegularExpressions;
using System.Linq;

namespace EmployeeRegister.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region �R���X�g���N�^
        public MainViewModel()
        {
            YearItems = new List<int>();
            MonthItems = new List<int>();
            DayItems = new List<int>();
            ItemsIsEnabled = true;
            YearSelIndex = -1;
            MonthSelIndex = -1;
            DaySelIndex = -1;
            SexSelIndex = -1;
            ModeIsChecked = false;

            GetDepartmentInfos();

            SelectCommand = new RelayCommand(() => { SelectEmployeeInfo(); });
            RegistCommand = new RelayCommand(() => { RegistEmployeeInfo(); });
            ClearCommand = new RelayCommand(() => { ClearAll(); });
            ModeChangeCommand = new RelayCommand<bool>((m) => { ApplyBase(!(bool)m); });

            InitDateControl();
        }
        #endregion

        #region �R�}���h
        public RelayCommand SelectCommand { get; private set; }
        public RelayCommand RegistCommand { get; private set; }
        public RelayCommand ClearCommand { get; private set; }
        public RelayCommand<bool> ModeChangeCommand { get; private set; }
        #endregion

        #region �v���p�e�B
        private string _employeeNo;
        private bool _employeeNoEnabled;
        private string _firstName;
        private string _lastName;
        private string _fullName;
        private string _postalCode;
        private string _address;
        private string _tel;
        private DateTime _birthday;
        private int _sexSelIndex;
        private List<Model.DepartmentInfo> _departmentInfos;
        private List<string> _departmentNames;
        private int _departmentIndex;
        private string _remarks;
        private Visibility _selectVisibility;
        private List<int> _yearItems;
        private List<int> _monthItems;
        private List<int> _dayItems;
        private int _yearSelIndex;
        private int _monthSelIndex;
        private int _daySelIndex;
        private bool _itemsIsEnabled;
        private string _mode;
        private bool _modeIsChanged;
        private MaterialDialogViewModel _daialogVM;
        private bool _isDialogOpen;

        public string EmployeeNo
        {
            get { return _employeeNo; }
            set
            {
                var tmp = 0;
                if (string.IsNullOrEmpty(value) || (int.TryParse(value, out tmp) && value.Length < 6))
                { _employeeNo = value; }
                RaisePropertyChanged(nameof(EmployeeNo));
            }
        }

        public bool EmployeeNoEnabled
        {
            get { return _employeeNoEnabled; }
            set
            {
                _employeeNoEnabled = value;
                RaisePropertyChanged(nameof(EmployeeNoEnabled));
            }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (Regex.IsMatch(value, @"[!-/;]") || value.Length > 10) return;
                _firstName = value;
                FullName = value + " " + _lastName;
                RaisePropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (Regex.IsMatch(value, @"[!-/;]") || value.Length > 10) return;
                _lastName = value;
                FullName = _firstName + " " + value;
                RaisePropertyChanged(nameof(LastName));
            }

        }

        public string FullName
        {
            get { return _fullName; }
            set
            {
                if (Regex.IsMatch(value, @"[!-/;]") || value.Length > 25) return;
                _fullName = value;
                RaisePropertyChanged(nameof(FullName));
            }
        }

        public string PostalCode
        {
            get { return _postalCode; }
            set
            {
                var tmp = 0;
                if (string.IsNullOrEmpty(value) || (int.TryParse(value, out tmp) && value.Length < 8))
                {
                    _postalCode = value;
                    RaisePropertyChanged(nameof(PostalCode));
                }
            }
        }

        public string Address
        {
            get { return _address; }
            set
            {
                if (Regex.IsMatch(value, @"[!';]") || value.Length > 60) return;
                _address = value;
                RaisePropertyChanged(nameof(Address));
            }
        }

        public string Tel
        {
            get { return _tel; }
            set
            {
                if (string.IsNullOrEmpty(value) || (!Regex.IsMatch(value, @"[!'/;a-z]")) && value.Length < 12)
                {
                    _tel = value;
                    RaisePropertyChanged(nameof(Tel));
                }
            }
        }

        public DateTime BirthDay
        {
            get { return _birthday; }
            set
            {
                _birthday = value;
                RaisePropertyChanged(nameof(BirthDay));
            }
        }

        /// <summary>
        /// 0:�j 1:��
        /// </summary>
        public int SexSelIndex
        {
            get { return _sexSelIndex; }
            set
            {
                _sexSelIndex = value;
                RaisePropertyChanged(nameof(SexSelIndex));
            }
        }

        public List<Model.DepartmentInfo> DepartmentInfos
        {
            get { return _departmentInfos; }
            set
            {
                _departmentInfos = value;
                RaisePropertyChanged(nameof(DepartmentInfos));
            }
        }

        public List<string> DepartmentNames
        {
            get { return _departmentNames; }
            set
            {
                _departmentNames = value;
                RaisePropertyChanged(nameof(DepartmentNames));
            }
        }

        public int DepartmentIndex
        {
            get { return _departmentIndex; }
            set
            {
                _departmentIndex = value;
                RaisePropertyChanged(nameof(DepartmentIndex));
            }
        }

        public string Remarks
        {
            get { return _remarks; }
            set
            {
                if ((Regex.IsMatch(value, @"[!-/;]") || value.Length > 60)) return;
                _remarks = value;
                RaisePropertyChanged(nameof(Remarks));
            }
        }

        public Visibility SelectVisibility
        {
            get { return _selectVisibility; }
            set
            {
                _selectVisibility = value;
                RaisePropertyChanged(nameof(SelectVisibility));
            }
        }

        public List<int> YearItems
        {
            get { return _yearItems; }
            set
            {
                _yearItems = value;
                RaisePropertyChanged(nameof(YearItems));
            }
        }

        public List<int> MonthItems
        {
            get { return _monthItems; }
            set
            {
                _monthItems = value;
                RaisePropertyChanged(nameof(MonthItems));
            }
        }

        public List<int> DayItems
        {
            get { return _dayItems; }
            set
            {
                _dayItems = value;
                RaisePropertyChanged(nameof(DayItems));
            }
        }

        public int YearSelIndex
        {
            get { return _yearSelIndex; }
            set
            {
                _yearSelIndex = value;
                RaisePropertyChanged(nameof(YearSelIndex));
            }
        }

        public int MonthSelIndex
        {
            get { return _monthSelIndex; }
            set
            {
                _monthSelIndex = value;
                RaisePropertyChanged(nameof(MonthSelIndex));
            }
        }

        public int DaySelIndex
        {
            get { return _daySelIndex; }
            set
            {
                _daySelIndex = value;
                RaisePropertyChanged(nameof(DaySelIndex));
            }
        }

        /// <summary>
        /// �]�ƈ��R�[�h�ȊO�̃R���g�[����Enabled���
        /// </summary>
        public bool ItemsIsEnabled
        {
            get { return _itemsIsEnabled; }
            set
            {
                _itemsIsEnabled = value;
                RaisePropertyChanged(nameof(ItemsIsEnabled));
            }
        }

        public string Mode
        {
            get { return _mode; }
            set
            {
                _mode = value;
                RaisePropertyChanged(nameof(Mode));
            }
        }

        public bool ModeIsChecked
        {
            get { return _modeIsChanged; }
            set
            {
                _modeIsChanged = value;
                ChangeMode(value);
                RaisePropertyChanged(nameof(ModeIsChecked));
            }
        }

        public MaterialDialogViewModel DialogVM
        {
            get { return _daialogVM; }
            set
            {
                _daialogVM = value;
                RaisePropertyChanged(nameof(DialogVM));
            }
        }

        public bool IsDialogOpen
        {
            get { return _isDialogOpen; }
            set
            {
                _isDialogOpen = value;
                RaisePropertyChanged(nameof(IsDialogOpen));
            }
        }
        #endregion

        #region �֘A����

        /// <summary>
        /// �w�i�F�؂�ւ�
        /// </summary>
        /// <param name="isWhite"></param>
        private static void ApplyBase(bool isWhite)
        {
            new PaletteHelper().SetLightDark(isWhite);
        }

        /// <summary>
        /// ���N�����I�����ݒ�
        /// </summary>
        private void InitDateControl()
        {
            for (int i = 2003; i > 1950; i--)
            {
                YearItems.Add(i);
            }

            for (int i = 1; i < 13; i++)
            {
                MonthItems.Add(i);
            }

            for (int i = 1; i < 32; i++)
            {
                DayItems.Add(i);
            }
        }

        /// <summary>
        /// �N���A
        /// </summary>
        private void ClearAll()
        {
            EmployeeNo = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            FullName = string.Empty;
            PostalCode = string.Empty;
            Address = string.Empty;
            Tel = string.Empty;
            SexSelIndex = -1;
            DepartmentIndex = -1;
            Remarks = string.Empty;
            YearSelIndex = -1;
            MonthSelIndex = -1;
            DaySelIndex = -1;

            if (ModeIsChecked)
            {
                // �X�V
                ItemsIsEnabled = false;
            }
        }

        /// <summary>
        /// �X�V/�V�K �؂�ւ�
        /// </summary>
        /// <param name="isUpdate"></param>
        private void ChangeMode(bool isUpdate)
        {
            EmployeeNoEnabled = isUpdate;
            ItemsIsEnabled = !isUpdate;
            Mode = isUpdate ? "�X�V" : "�V�K";
            SelectVisibility = isUpdate ? Visibility.Visible : Visibility.Collapsed;
            ClearAll();
        }

        /// <summary>
        /// �Ј����擾
        /// </summary>
        private void SelectEmployeeInfo()
        {
            if (string.IsNullOrEmpty(EmployeeNo))
            {
                DialogVM = new MaterialDialogViewModel("Error", "�]�ƈ��R�[�h����͂��Ă�������.");
                IsDialogOpen = true;
                return;
            }

            var emp = new Model.EmployeeInfo();
            var selRet = emp.GetInfoFromDb(int.Parse(EmployeeNo));

            if (!selRet)
            {
                DialogVM = new MaterialDialogViewModel("Error", "�]�ƈ����̎擾�Ɏ��s���܂���.���O���m�F���Ă�������.");
                IsDialogOpen = true;
                return;
            }

            if (emp.EMPLOYEE_NO == -1)
            {
                DialogVM = new MaterialDialogViewModel("Info", "�Y������]�ƈ���񂪂���܂���.");
                IsDialogOpen = true;
                return;
            }

            FirstName = emp.FIRST_NAME;
            LastName = emp.LAST_NAME;
            FullName = emp.FULL_NAME;
            PostalCode = emp.POSTAL_CODE;
            Address = emp.ADDRESS;
            Tel = emp.TEL;
            YearSelIndex = YearItems.FindIndex(m => m == emp.BIRTHDAY.Year);
            MonthSelIndex = MonthItems.FindIndex(m => m == emp.BIRTHDAY.Month);
            DaySelIndex = DayItems.FindIndex(m => m == emp.BIRTHDAY.Day);
            SexSelIndex = emp.SEX == "�j" ? 0 : 1;
            DepartmentIndex = DepartmentInfos.FindIndex(m => m.DEPARTMENT_CODE == emp.DEPARTMENT_CODE);
            Remarks = emp.REMARKS;

            ItemsIsEnabled = true;

            return;
        }

        /// <summary>
        /// �������擾
        /// </summary>
        private void GetDepartmentInfos()
        {
            var dep = new Model.DepartmentInfo();
            var items = dep.GetAllInfoFromDb();

            if (items == null)
            {
                DialogVM = new MaterialDialogViewModel("Error", "������񂪎擾�ł��܂���ł���.���O���m�F���Ă�������.");
                IsDialogOpen = true;
                return;
            }

            if (items.Count == 0)
            {
                DialogVM = new MaterialDialogViewModel("Info", "������񂪓o�^����Ă��܂���.");
                IsDialogOpen = true;
                return;
            }

            DepartmentInfos = items;
            DepartmentNames = items.Select(m => m.DEPARTMENT_NAME).ToList();
        }

        /// <summary>
        /// �o�^/�X�V
        /// </summary>
        private void RegistEmployeeInfo()
        {
            if (!CheckInputControl()) { return; }

            var emp = new Model.EmployeeInfo();
            emp.FIRST_NAME = FirstName;
            emp.LAST_NAME = LastName;
            emp.FULL_NAME = FullName;
            emp.DEPARTMENT_CODE = DepartmentInfos[DepartmentIndex].DEPARTMENT_CODE;
            emp.POSTAL_CODE = PostalCode;
            emp.ADDRESS = Address;
            emp.TEL = Tel;
            emp.BIRTHDAY = new DateTime(YearItems[YearSelIndex], MonthItems[MonthSelIndex], DayItems[DaySelIndex]);
            emp.SEX = SexSelIndex == 0 ? "�j" : "��";
            emp.REMARKS = Remarks;

            if (ModeIsChecked)
            {
                // �X�V
                emp.EMPLOYEE_NO = int.Parse(EmployeeNo);
                emp.UPDATE_DATE = DateTime.Now;

                var beforeEmp = new Model.EmployeeInfo();
                beforeEmp.GetInfoFromDb(int.Parse(EmployeeNo));
                if (beforeEmp.EMPLOYEE_NO == -1)
                {
                    DialogVM = new MaterialDialogViewModel("Error", "�Y������]�Ə�񂪂���܂���.�]�ƈ��R�[�h���m�F���Ă�������.");
                    IsDialogOpen = true;
                    return;
                }

                if (!emp.Update())
                {
                    DialogVM = new MaterialDialogViewModel("Error", "�X�V�����Ɏ��s���܂���.���O���m�F���Ă�������.");
                    IsDialogOpen = true;
                    return;
                };

                DialogVM = new MaterialDialogViewModel("Success!", "�X�V���܂���.");
                IsDialogOpen = true;
                return;
            }
            else
            {
                // �V�K
                emp.REGIST_DATE = DateTime.Now;
                if (!emp.Insert())
                {
                    DialogVM = new MaterialDialogViewModel("Error", "�o�^�����Ɏ��s���܂���.���O���m�F���Ă�������.");
                    IsDialogOpen = true;
                    return;
                };

                DialogVM = new MaterialDialogViewModel("Success!", "�o�^���܂���.");
                IsDialogOpen = true;
                return;
            }
        }

        /// <summary>
        /// ���͒l�`�F�b�N
        /// ���͕����̓v���p�e�B�Ő������Ă邩���`�F�b�N��������
        /// </summary>
        /// <returns></returns>
        private bool CheckInputControl()
        {
            if (string.IsNullOrWhiteSpace(FirstName) ||
                string.IsNullOrWhiteSpace(LastName) ||
                string.IsNullOrWhiteSpace(FullName) ||
                string.IsNullOrWhiteSpace(PostalCode) ||
                string.IsNullOrWhiteSpace(Address) ||
                string.IsNullOrWhiteSpace(Tel) ||
                YearSelIndex == -1 ||
                MonthSelIndex == -1 ||
                DaySelIndex == -1 ||
                SexSelIndex == -1 ||
                DepartmentIndex == -1)
            {
                DialogVM = new MaterialDialogViewModel("Error", "���͂���Ă��Ȃ����ڂ����݂��܂�.");
                IsDialogOpen = true;
                return false;
            }

            if (ModeIsChecked && string.IsNullOrWhiteSpace(EmployeeNo))
            {
                DialogVM = new MaterialDialogViewModel("Error", "���͂���Ă��Ȃ����ڂ����݂��܂�.");
                IsDialogOpen = true;
                return false;
            }

            return true;
        }

        /// <summary>
        /// �X�V����
        /// </summary>
        private void Update(Model.EmployeeInfo emp)
        {
        }

        /// <summary>
        /// �V�K�o�^����
        /// </summary>
        private void Insert(Model.EmployeeInfo emp)
        {

        }
        #endregion
    }
}