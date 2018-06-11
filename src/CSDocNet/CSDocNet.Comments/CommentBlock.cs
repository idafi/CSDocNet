using System.Collections.Generic;
using CSDocNet.Collections;

namespace CSDocNet.Comments
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
            Nodes = nodes ?? Empty<CommentNode>.List;
            
            Text = text ?? Empty<string>.List;
            Blocks = blocks ?? Empty<CommentBlock>.List;
            Lists = lists ?? Empty<CommentList>.List;
        }

        public static CommentBlock Empty
            => new CommentBlock(null, null, null, null);
    }
}