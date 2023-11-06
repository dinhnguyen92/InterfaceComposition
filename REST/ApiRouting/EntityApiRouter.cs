using InterfaceComposition.REST.ApiRouting.UrlSegments;
using System;
using System.Collections.Generic;

namespace InterfaceComposition.REST.ApiRouting
{
    public class EntityApiRouter
    {
        protected CompositeUrlSegment EntityControllerRoute { get; }

        public EntityApiRouter(CompositeUrlSegment entityControllerRoute)
        {
            if (entityControllerRoute.IsEmpty())
            {
                throw new ArgumentException("Entity root controller route cannot be empty");
            }
            EntityControllerRoute = entityControllerRoute;
        }

        public List<string> GetRouteParameterNames() =>
            EntityControllerRoute.GetParameterizedSegments()
                .ConvertAll(segment => segment.ParameterName);


        #region Entity Query Routes

        public CompositeUrlSegment EntityWithIdRoute(object id) =>
            EntityControllerRoute.AddSegment(new EntityIdUrlSegment(id));

        public CompositeUrlSegment EntityExistenceCheckRoute =>
            EntityControllerRoute.AddSegment(LiteralUrlSegment.EntityExistenceCheckUrlSegment);

        public CompositeUrlSegment EntityExistenceCheckByIdRoute(object id) =>
            EntityExistenceCheckRoute.AddSegment(new EntityIdUrlSegment(id));

        public CompositeUrlSegment EntityListRoute => EntityControllerRoute;

        public CompositeUrlSegment EntityRetrievalByUrlParamsRoute => EntityControllerRoute;

        public CompositeUrlSegment EntitySearchRoute =>
            EntityControllerRoute.AddSegment(LiteralUrlSegment.EntitySearchUrlSegment);

        #endregion


        #region Entity Command Routes

        public CompositeUrlSegment EntityCreationRoute => EntityControllerRoute;

        public CompositeUrlSegment EntityUpdateByIdRoute(object id) => EntityWithIdRoute(id);

        public CompositeUrlSegment EntityUpdateByUrlParamsRoute => EntityControllerRoute;

        public CompositeUrlSegment EntityUpdateByFilterPayloadRoute => EntityControllerRoute;

        public CompositeUrlSegment EntityUpsertByUrlParamsRoute => EntityControllerRoute;

        public CompositeUrlSegment EntityUpsertByFilterPayloadRoute => EntityControllerRoute;

        public CompositeUrlSegment EntityUpsertByIdRoute(object id) => EntityWithIdRoute(id);

        public CompositeUrlSegment EntityDeletionByIdRoute(object id) => EntityWithIdRoute(id);

        public CompositeUrlSegment EntityDeletionByUrlParamsRoute => EntityControllerRoute;

        #endregion
    }
}
