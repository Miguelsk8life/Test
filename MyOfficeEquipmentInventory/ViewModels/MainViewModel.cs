using MyOfficeEquipmentInventory.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MyOfficeEquipmentInventory.ViewModels
{
    /// <summary>
    /// Основная ViewModel приложения
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly EquipmentRepository _repository;
        private Equipment _selectedEquipment;

        /// <summary>
        /// Коллекция всего оборудования
        /// </summary>
        public ObservableCollection<Equipment> Equipments { get; }

        /// <summary>
        /// Выбранное в данный момент оборудование
        /// </summary>
        public Equipment SelectedEquipment
        {
            get => _selectedEquipment;
            set
            {
                _selectedEquipment = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsEquipmentSelected));
            }
        }

        /// <summary>
        /// Флаг, указывающий что оборудование выбрано
        /// </summary>
        public bool IsEquipmentSelected => SelectedEquipment != null;

        // Команды для взаимодействия с UI
        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }

        /// <summary>
        /// Конструктор ViewModel
        /// </summary>
        public MainViewModel()
        {
            _repository = new EquipmentRepository();
            Equipments = _repository.GetAllEquipments();

            // Инициализация команд
            AddCommand = new RelayCommand(AddEquipment);
            UpdateCommand = new RelayCommand(UpdateEquipment, CanExecuteUpdateOrDelete);
            DeleteCommand = new RelayCommand(DeleteEquipment, CanExecuteUpdateOrDelete);
        }

        /// <summary>
        /// Добавление нового оборудования
        /// </summary>
        private void AddEquipment(object parameter)
        {
            var newEquipment = new Equipment
            {
                Id = GetNextId(),
                Name = "New Equipment " + DateTime.Now.ToString("HHmmss"),
                Type = EquipmentType.Printer,
                Status = StatusType.InStock
            };
            _repository.AddEquipment(newEquipment);
            SelectedEquipment = newEquipment;
            OnPropertyChanged(nameof(Equipments));
        }

        /// <summary>
        /// Генерация следующего ID для нового оборудования
        /// </summary>
        private int GetNextId()
        {
            return Equipments.Count > 0 ? Equipments.Max(e => e.Id) + 1 : 1;
        }

        /// <summary>
        /// Обновление данных выбранного оборудования
        /// </summary>
        private void UpdateEquipment(object parameter)
        {
            if (SelectedEquipment != null)
            {
                _repository.UpdateEquipment(SelectedEquipment);
                var index = Equipments.IndexOf(SelectedEquipment);
                Equipments[index] = SelectedEquipment;
                OnPropertyChanged(nameof(Equipments));
                OnPropertyChanged(nameof(SelectedEquipment));
            }
        }

        /// <summary>
        /// Удаление выбранного оборудования
        /// </summary>
        private void DeleteEquipment(object parameter)
        {
            if (SelectedEquipment != null)
            {
                _repository.DeleteEquipment(SelectedEquipment.Id);
                SelectedEquipment = null;
            }
        }

        /// <summary>
        /// Проверка возможности выполнения команд обновления/удаления
        /// </summary>
        private bool CanExecuteUpdateOrDelete(object parameter)
        {
            return IsEquipmentSelected;
        }

        /// <summary>
        /// Событие изменения свойства
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Метод вызова события PropertyChanged
        /// </summary>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}