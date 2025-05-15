using BogglerUltimate.Utils;

namespace BogglerUltimate.Trie;

public class TrieNode
{
    internal char Letter { get; set; }
    internal bool IsWord { get; set; }
    internal TrieNode?[] Children { get; set; } = new TrieNode?[26];

    internal static TrieNode CreateNode(char c)
    {
        var node = new TrieNode();
        node.Letter = c;
        node.IsWord = false;

        return node;
    }

    public bool HasChild(char c)
    {
        int index = Tools.Index(c);
        
        // Ensures Index lands somewhere between 0(A) and 25(Z) and is not null
        return index is >= 0 and < 26 && Children[index] != null;    
    }

    public TrieNode AddChild(char c)
    {
        int index = Tools.Index(c);
        if (Children[index] == null) 
            Children[index] = new TrieNode { Letter = c };
        
        return Children[index]!;
        
    }
}