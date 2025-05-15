using BogglerUltimate.Trie;
using BogglerUltimate.Utils;

namespace BogglerUltimate.Iterators;

public class TrieIterator
{
    internal TrieNode _current;

    // Should this be a factory method
    public TrieIterator(BoggleTrie trie)
    {
        _current = trie._root;
    }
    
    public void Traverse(char c)
    {
        int index = Tools.Index(c);

        if (_current.HasChild(c))
            _current = _current.Children[index];
    }

    public bool IsWord() => _current.IsWord;
}