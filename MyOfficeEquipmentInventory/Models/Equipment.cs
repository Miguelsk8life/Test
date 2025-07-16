using System.ComponentModel;

namespace MyOfficeEquipmentInventory.Models
{
    /// <summary>
    /// Класс, представляющий модель офисного оборудования
    /// </summary>
    public class Equipment : INotifyPropertyChanged
    {
        private string _name;
        private EquipmentType _type;
        private StatusType _status;

        /// <summary>
        /// Уникальный идентификатор оборудования
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование оборудования
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        /// <summary>
        /// Тип оборудования (принтер, сканер, монитор)
        /// </summary>
        public EquipmentType Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged(nameof(Type));
            }
        }

        /// <summary>
        /// Текущий статус оборудования
        /// </summary>
        public StatusType Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        /// <summary>
        /// Событие, возникающее при изменении свойств
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Метод для вызова события PropertyChanged
        /// </summary>
        /// <param name="propertyName">Имя изменившегося свойства</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}