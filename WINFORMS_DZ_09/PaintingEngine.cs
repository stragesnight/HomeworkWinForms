using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace WINFORMS_DZ_09
{
    // Движок рисования, ответственный за непосредственную отрисовку графики
    // а также за внутренние параметры рисования
    public partial class PaintingEngine
    {
        public enum DrawingInstrument
        {
            Pen,
            Rectangle,
            Ellipse,
            Circle,
            Triangle
        }

        // Родительские элементы

        // "Холст" для рисования
        private Control _parent;
        // картинка для внутреннего хранения данных
        private Image _image;

        // Текущие настройки рисования

        private DrawingInstrument _instrument = DrawingInstrument.Pen;
        private Color _color = Color.White;
        private Brush _brush = Brushes.White;
        private Pen _pen = new Pen(Color.White, 2f);

        private Point _mouseOriginPosition = new Point(0, 0);
        private Point _mousePosition = new Point(0, 0);
        private int _penWeight = 2;
        private bool _isDrawing = false;
        private bool _shouldUpdateImage = false;

        public PaintingEngine(Form1 form, Control parent)
        {
            // подписка на события родителя

            form.InstrumentIndexChanged += OnInstrumentIndexChanged;
            form.ColorIndexChanged += OnColorIndexChanged;
            form.MouseStateChanged += OnMouseStateChanged;
            form.MouseMoved += OnMouseMoved;
            form.PaintEvent += OnPaint;
            form.PenWeightChanged += OnPenWeightChanged;

            _parent = parent;
            InitializeEmptyImage();
        }

        // Загрузка изображения из файла
        public void SetImage(string filename)
        {
            _image = Image.FromFile(filename);
            _parent.ClientSize = _image.Size;
            OnPaint();
        }

        // Сохранение изображения в файл
        public void SaveImage(string filename, ImageFormat format)
        {
            _image.Save(filename, format);
        }

        // Инициализация пустого изображения
        public void InitializeEmptyImage()
        {
            _image = new Bitmap(_parent.Width, _parent.Height, PixelFormat.Format24bppRgb);
            Graphics.FromImage(_image).Clear(SystemColors.Control);
        }

        private void OnInstrumentIndexChanged(ListBox listBox)
        {
            bool success = Enum.TryParse(
                listBox.SelectedItem.ToString(), out DrawingInstrument result
            );
            if (success)
                _instrument = result;
        }

        private void OnColorIndexChanged(ListBox listBox)
        {
            bool success = Enum.TryParse(
                listBox.SelectedItem.ToString(), out KnownColor result   
            );
            if (success)
            { 
                _color = Color.FromKnownColor(result);
                _brush = new SolidBrush(_color);
                _pen = new Pen(_color, _penWeight);
            }
        }

        private void OnMouseStateChanged(Point pos, bool pressed)
        {
            // отрисовать изменения на изображении
            // при завершении движения мыши
            if (_isDrawing)
            {
                _shouldUpdateImage = true;
                OnPaint();
            }

            _isDrawing = pressed;
            _mouseOriginPosition = pos;
            _penPoints.Clear();
        }

        private void OnMouseMoved(Point pos)
        {
            _mousePosition = pos;
        }

        private void OnPaint()
        {
            if (!_isDrawing)
                return;

            Graphics tmp;

            // выбрать "холст" для рисования
            if (_shouldUpdateImage)
            {
                // отрисовка графики непосредственно на изображении
                tmp = Graphics.FromImage(_image);
                _shouldUpdateImage = false;
            }
            else
            {
                // отрисовка на форме (на изображение не влияет)
                tmp = _parent.CreateGraphics();
                tmp.Clear(_parent.BackColor);
            }

            tmp.DrawImageUnscaled(_image, 0, 0);

            // выбор действия в зависимости от выбранного инструмента
            // (методы описаны в PaintingEngine_Figures.cs
            switch (_instrument)
            {
                case DrawingInstrument.Pen:
                    HandlePenPaint(tmp);
                    break;
                case DrawingInstrument.Rectangle:
                    HandleRectanglePaint(tmp);
                    break;
                case DrawingInstrument.Ellipse:
                    HandleEllipsePaint(tmp);
                    break;
                case DrawingInstrument.Circle:
                    HandleCirclePaint(tmp);
                    break;
                case DrawingInstrument.Triangle:
                    HandleTrianglePaint(tmp);
                    break;
                default:
                    throw new Exception(
                        $"Undeclared DrawingInstrument \"{_instrument}\""
                    );
                    break;
            }

            tmp.Dispose();
        }

        private void OnPenWeightChanged(int value)
        {
            _penWeight = value;
            _pen = new Pen(_color, value);
        }
    }
}
