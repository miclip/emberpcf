﻿using NJsonApi.Serialization.Representations;

namespace NJsonApi.Serialization
{
    public interface ILinkBuilder
    {
        ILink FindResourceSelfLink(Context context, string resourceId, IResourceMapping resourceMapping);

        ILink RelationshipSelfLink(Context context, string resourceId, IResourceMapping resourceMapping, IRelationshipMapping linkMapping);

        ILink RelationshipRelatedLink(Context context, string resourceId, IResourceMapping resourceMapping, IRelationshipMapping linkMapping);
    }
}