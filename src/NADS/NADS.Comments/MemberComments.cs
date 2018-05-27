using System.Collections.Generic;
using NADS.Collections;

namespace NADS.Comments
{
    public readonly struct MemberComments
    {
        public readonly string Name;
        
        public readonly CommentBlock Summary;
        public readonly CommentBlock Remarks;
        public readonly CommentBlock Value;
        public readonly CommentBlock Returns;
        public readonly CommentBlock Example;

        public readonly IReadOnlyList<ParamComments> Params;
        public readonly IReadOnlyList<ParamComments> TypeParams;
        public readonly IReadOnlyList<ParamComments> Exceptions;
        public readonly IReadOnlyList<ParamComments> Permissions;

        public MemberComments(string name, in CommentBlock summary, in CommentBlock remarks,
            in CommentBlock value, in CommentBlock returns, in CommentBlock example,
            IReadOnlyList<ParamComments> parameters, IReadOnlyList<ParamComments> typeParams,
            IReadOnlyList<ParamComments> exceptions, IReadOnlyList<ParamComments> permissions)
        {
            Name = name ?? "";

            Summary = summary;
            Remarks = remarks;
            Value = value;
            Returns = returns;
            Example = example;

            Params = parameters ?? Empty<ParamComments>.EmptyList;
            TypeParams = typeParams ?? Empty<ParamComments>.EmptyList;
            Exceptions = exceptions ?? Empty<ParamComments>.EmptyList;
            Permissions = permissions ?? Empty<ParamComments>.EmptyList;
        }

        public static MemberComments Empty
            => new MemberComments(null, CommentBlock.Empty, CommentBlock.Empty,
                CommentBlock.Empty, CommentBlock.Empty, CommentBlock.Empty,
                null, null, null, null);
        
        public bool IsEmpty
            => Name == ""
            && Summary.Nodes.Count == 0
            && Remarks.Nodes.Count == 0
            && Value.Nodes.Count == 0
            && Returns.Nodes.Count == 0
            && Example.Nodes.Count == 0
            && Params.Count == 0
            && TypeParams.Count == 0
            && Exceptions.Count == 0
            && Permissions.Count == 0;
    }
}