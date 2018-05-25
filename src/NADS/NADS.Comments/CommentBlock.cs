using System.Collections.Generic;
using NADS.Collections;

namespace NADS.Comments
{
    public readonly struct CommentBlock
    {
        public readonly IReadOnlyList<CommentNode> Nodes;

        public readonly IReadOnlyList<string> Text;
        public readonly IReadOnlyList<CommentBlock> Blocks;
        public readonly IReadOnlyList<CommentList> Lists;

        public CommentBlock(IReadOnlyList<CommentNode> nodes,
            IReadOnlyList<string> text, IReadOnlyList<CommentBlock> blocks,
            IReadOnlyList<CommentList> lists)
        {
            Nodes = nodes ?? Empty<CommentNode>.EmptyList;
            
            Text = text ?? Empty<string>.EmptyList;
            Blocks = blocks ?? Empty<CommentBlock>.EmptyList;
            Lists = lists ?? Empty<CommentList>.EmptyList;
        }
    }
}