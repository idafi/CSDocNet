using System.Collections.Generic;
using NADS.Collections;

namespace NADS.Comments
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
            Items = items ?? Empty<CommentListItem>.EmptyList;
        }

        public static CommentList Empty
            => new CommentList(default, CommentListItem.Empty, Empty<CommentListItem>.EmptyList);
    }
}