using System.Collections.Generic;
using NADS.Collections;

namespace NADS.Comments
{
    public readonly struct CommentBlock
    {
        public readonly IReadOnlyList<CommentNode> Nodes;

        public readonly IReadOnlyList<string> Text;
        public readonly IReadOnlyList<CommentBlock> Blocks;

        public CommentBlock(IReadOnlyList<CommentNode> nodes,
            IReadOnlyList<string> text, IReadOnlyList<CommentBlock> blocks)
        {
            Nodes = nodes ?? Empty<CommentNode>.EmptyList;
            
            Text = text ?? Empty<string>.EmptyList;
            Blocks = blocks ?? Empty<CommentBlock>.EmptyList;
        }
    }
}