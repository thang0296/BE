//using System;
//using System.Collections.Generic;
//using Web6.Enti;
//using Microsoft.EntityFrameworkCore;


//namespace Web6.Enti
//{
//    public partial class WebAction
//    {
//        public WebAction()
//        {
//            RoleActions = new HashSet<RoleAction>();
//        }

//        public int Id { get; set; }
//        public string Name { get; set; } = null!;
//        public string Description { get; set; } = null!;

//        public virtual ICollection<RoleAction> RoleActions { get; set; }
//    }


//public static class WebActionEndpoints
//{
//	public static void MapWebActionEndpoints (this IEndpointRouteBuilder routes)
//    {
//        routes.MapGet("/api/WebAction", async (BANHMIContext db) =>
//        {
//            return await db.WebActions.ToListAsync();
//        })
//        .WithName("GetAllWebActions")
//        .Produces<List<WebAction>>(StatusCodes.Status200OK);

//        routes.MapGet("/api/WebAction/{id}", async (int Id, BANHMIContext db) =>
//        {
//            return await db.WebActions.FindAsync(Id)
//                is WebAction model
//                    ? Results.Ok(model)
//                    : Results.NotFound();
//        })
//        .WithName("GetWebActionById")
//        .Produces<WebAction>(StatusCodes.Status200OK)
//        .Produces(StatusCodes.Status404NotFound);

//        routes.MapPut("/api/WebAction/{id}", async (int Id, WebAction webAction, BANHMIContext db) =>
//        {
//            var foundModel = await db.WebActions.FindAsync(Id);

//            if (foundModel is null)
//            {
//                return Results.NotFound();
//            }

//            db.Update(webAction);

//            await db.SaveChangesAsync();

//            return Results.NoContent();
//        })
//        .WithName("UpdateWebAction")
//        .Produces(StatusCodes.Status404NotFound)
//        .Produces(StatusCodes.Status204NoContent);

//        routes.MapPost("/api/WebAction/", async (WebAction webAction, BANHMIContext db) =>
//        {
//            db.WebActions.Add(webAction);
//            await db.SaveChangesAsync();
//            return Results.Created($"/WebActions/{webAction.Id}", webAction);
//        })
//        .WithName("CreateWebAction")
//        .Produces<WebAction>(StatusCodes.Status201Created);


//        routes.MapDelete("/api/WebAction/{id}", async (int Id, BANHMIContext db) =>
//        {
//            if (await db.WebActions.FindAsync(Id) is WebAction webAction)
//            {
//                db.WebActions.Remove(webAction);
//                await db.SaveChangesAsync();
//                return Results.Ok(webAction);
//            }

//            return Results.NotFound();
//        })
//        .WithName("DeleteWebAction")
//        .Produces<WebAction>(StatusCodes.Status200OK)
//        .Produces(StatusCodes.Status404NotFound);
//    }
//}}
