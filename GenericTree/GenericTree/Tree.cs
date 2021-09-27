using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GenericTree
{
    public class Tree<T> : ICollection<T>
    {
        public static string PathDelimiter = "/";
        private T _value;
        private Tree<T> _parent;
        private List<Tree<T>> _children;
        private static Func<T, string> _nameConverter;

        public Tree(T value)
        {
            _value = value;
            _parent = null;
            _children = new List<Tree<T>>();
            if(_nameConverter == null)
                _nameConverter = (a => a.ToString());
        }
        public Tree(T value, Func<T, string> nameConverter)
        {
            _value = value;
            _parent = null;
            _children = new List<Tree<T>>();
            _nameConverter = nameConverter;
        }

        private Tree(T value, Tree<T> parent)
        {
            _value = value;
            _parent = parent;
            _children = new List<Tree<T>>();
        }

        public int Count
        {
            get { return _children.Count(); }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }
        public void Add(T item)
        {
            _children.Add(new Tree<T>(item, this));
        }

        public void Add(Tree<T> item)
        {
            item._parent = this;
            _children.Add(item);
        }

        public void Clear()
        {
            _children.Clear();
        }

        public bool Contains(T item)
        {
            return _children.Any(a => a._value.Equals(item)) || _children.Any(a => a.Contains(item));
        }
        public bool Contains(Func<T, bool> predicate)
        {
            return _children.Any(a => predicate(a._value)) || _children.Any(a => a.Contains(predicate));
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _children.Select(s=>s._value).ToArray().CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _children.Select(s=>s._value).GetEnumerator();
        }

        public bool Remove(T item)
        {
            var tree = _children.FirstOrDefault(a => a._value.Equals(item));
            if (tree == null)
                return false;
            return _children.Remove(tree);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public T this[int i]
        {
            get { return _children[i]._value; }
        }

        public void Draw()
        {
            Console.WriteLine(_nameConverter(_value));
            foreach(var item in GetNodesDepthFirst().Skip(1))
            {
                string s = _nameConverter(item._value);
                Tree<T> current = item;
                if (item.IsLastChild())
                {
                    s = "└──"+s;
                }
                else
                {
                    s = "├──"+s;
                }
                while (current._parent != null)
                {
                    current = current._parent;
                    if (current._parent != null)
                    {
                        if (current.IsLastChild())
                        {
                            s = "   " + s;
                        }
                        else
                        {
                            s = "│  " + s;
                        }
                    }
                }
                Console.WriteLine(s);
            }
        }

        public IEnumerable<T> GetItemsDepthFirst()
        {
            yield return _value;
            foreach (var item in _children)
            {
                foreach (var depth in item.GetItemsDepthFirst())
                {
                    yield return depth;
                }
            }
        }

        public IEnumerable<Tree<T>> GetNodesDepthFirst()
        {
            yield return this;
            foreach (var item in _children)
            {
                foreach (var depth in item.GetNodesDepthFirst())
                {
                    yield return depth;
                }
            }
        }

        public IEnumerable<T> GetItemsBreadthFirst(List<Tree<T>> children = null)
        {
            if (children == null)
            {
                yield return _value;
                children = _children;
            }
            var allChildren = new List<Tree<T>>();
            foreach (var item in children)
            {
                yield return item._value;
                allChildren.AddRange(item._children);
            }
            if (allChildren.Count > 0)
            {
                foreach (var item in GetItemsBreadthFirst(allChildren))
                {
                    yield return item;
                }
            }
        }

        public bool IsLastChild()
        {
            if (_parent == null)
                return true;
            return _parent._children.Last().Equals(this);
        }
        public T Find(string path, Tree<T> node = null)
        {
            path = path.Trim(PathDelimiter.ToArray());
            int index = path.IndexOf(PathDelimiter);
            if (index < 0)
                index = path.Length;
            string current = path.Substring(0, index);
            if (node == null)
            {
                if (current != _nameConverter(_value))
                    return default(T);
                node = this;
            }
            else
            {
                node = node._children.FirstOrDefault(a => _nameConverter(a._value) == current);
                if (node == null)
                    return default(T);
            }
            string remainder = path.Substring(index + PathDelimiter.Length - 1);
            if (string.IsNullOrEmpty(remainder))
                return node._value;
            return Find(remainder, node);
        }

        /*public IEnumerable<ItemWithLevel<T>> GetItemsDepthFirst()
        {
            return GetItemsDepthFirst(0);
        }

        public IEnumerable<ItemWithLevel<T>> GetItemsDepthFirst(int level)
        {
            yield return new ItemWithLevel<T>(_value, level);
            foreach (var item in _children)
            {
                foreach (var depth in item.GetItemsDepthFirst(level + 1))
                {
                    yield return depth;
                }
            }
        }*/



        /*public IEnumerable<T> GetItemsBreadthFirst()
        {
            yield return _value;
            foreach (var item in _children)
            {
                foreach (var depth in item.GetItemsBreadthFirst())
                {
                    yield return depth;
                }
            }
        }*/
    }

    /*public class ItemWithLevel<T>
    {
        private T _value;
        private int _level;
        public T Value { get { return _value; } }
        public int Level { get { return _level; } }
        public ItemWithLevel(T value, int level)
        {
            _value = value;
            _level = level;
        }
    }*/
}
