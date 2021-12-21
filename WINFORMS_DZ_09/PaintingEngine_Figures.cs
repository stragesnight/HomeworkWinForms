using System;
using System.Drawing;
using System.Collections.Generic;

namespace WINFORMS_DZ_09
{
    public partial class PaintingEngine
    {
        // "путь" указателя мыши с момента начала рисования
        private List<Point> _penPoints = new List<Point>();

        // Получить левый верхний угол фигуры
        private Point GetMinimumPoint()
        { 
            return new Point(
                Math.Min(_mouseOriginPosition.X, _mousePosition.X),
                Math.Min(_mouseOriginPosition.Y, _mousePosition.Y)
            );
        }

        // Получить правый нижний угол фигуры
        private Point GetMaximumPoint()
        { 
            return new Point(
                Math.Max(_mouseOriginPosition.X, _mousePosition.X),
                Math.Max(_mouseOriginPosition.Y, _mousePosition.Y)
            );
        }

        // Рисование ручкой
        private void HandlePenPaint(Graphics g)
        {
            // добавление новой точки в список
            _penPoints.Add(_mousePosition);

            // отрисовка пути, если точек больше чем одна
            if (_penPoints.Count > 1)
                g.DrawCurve(_pen, _penPoints.ToArray());
        }

        // Рисование Прямоугольника
        private void HandleRectanglePaint(Graphics g)
        {
            Point min = GetMinimumPoint();
            Point max = GetMaximumPoint();
            Size size = new Size(max.X - min.X, max.Y - min.Y);

            g.FillRectangle(_brush, new Rectangle(min, size));
        }

        // Рисование эллипса
        private void HandleEllipsePaint(Graphics g)
        {
            Point min = GetMinimumPoint();
            Point max = GetMaximumPoint();
            Size size = new Size(max.X - min.X, max.Y - min.Y);

            g.FillEllipse(_brush, new Rectangle(min, size));
        }

        // Рисование круга
        private void HandleCirclePaint(Graphics g)
        {
            Point min = GetMinimumPoint();
            Point max = GetMaximumPoint();
            Size size = new Size(max.X - min.X, max.X - min.X);

            g.FillEllipse(_brush, new Rectangle(min, size));
        }

        // Рисование треугольника
        private void HandleTrianglePaint(Graphics g)
        {
            Point min = GetMinimumPoint();
            Point max = GetMaximumPoint();
            Point[] points = {
                new Point(min.X, max.Y),
                new Point(min.X + ((max.X - min.X) / 2), min.Y),
                max
            };

            g.FillPolygon(_brush, points);
        }
    }
}
