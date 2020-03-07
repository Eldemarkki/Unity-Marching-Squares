using System;
using System.Collections.Generic;

namespace Eldemarkki.MarchingSquares
{
    public struct VoxelCorners<T> : IEquatable<VoxelCorners<T>>
    {
        public T Corner1 { get; set; }
        public T Corner2 { get; set; }
        public T Corner3 { get; set; }
        public T Corner4 { get; set; }

        public VoxelCorners(T c1, T c2, T c3, T c4)
        {
            Corner1 = c1;
            Corner2 = c2;
            Corner3 = c3;
            Corner4 = c4;
        }

        public T this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return Corner1;
                    case 1: return Corner2;
                    case 2: return Corner3;
                    case 3: return Corner4;
                    default: throw new IndexOutOfRangeException();
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        Corner1 = value;
                        break;
                    case 1:
                        Corner2 = value;
                        break;
                    case 2:
                        Corner3 = value;
                        break;
                    case 3:
                        Corner4 = value;
                        break;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }

        public bool Equals(VoxelCorners<T> other)
        {
            return EqualityComparer<T>.Default.Equals(Corner1, other.Corner1) &&
                   EqualityComparer<T>.Default.Equals(Corner2, other.Corner2) &&
                   EqualityComparer<T>.Default.Equals(Corner3, other.Corner3) &&
                   EqualityComparer<T>.Default.Equals(Corner4, other.Corner4);
        }
    }
}