namespace BogglerUltimate.Trie;

public class BoggleTrie
{
    internal readonly TrieNode _root = new TrieNode();
    public TrieNode CurrentNode { get; set; }

    public static BoggleTrie CreateBoggleTrie(IEnumerable<string> words)
    {
        BoggleTrie trie = new BoggleTrie();
        trie.CurrentNode = trie._root;

        foreach (string word in words)
        {
            trie.Insert(word);
        }

        return trie;
    }

    private void Insert(string word)
    {
        var node = _root;
        word = word.ToUpper();

        foreach (char c in word)
        {
            int index = c - 'A';

            if (!node.HasChild(c))
                node.AddChild(c);
            
            node = node.Children[index];
        }
        
        node.IsWord = true;
    }
}