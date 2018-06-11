using System.Collections.Generic;
using CSDocNet.Collections;

namespace CSDocNet.Comments
{
    public readonly struct CommentList
    {
        public readonly CommentListType Type;
        
        public readonly CommentListItem Header;
        public readonly IReadOnlyList<CommentListItem> Items;

        public CommentList(CommentListType type, in CommentListItem header,
            IReadOnlyList<CommentListItem> items)
        {
            Type = type;
            Header = header;
            Items = items ?? Empty<CommentListItem>.List;
        }

        public static CommentList Empty
            => new CommentList(default, CommentListItem.Empty, Empty<CommentListItem>.List);
    }
}