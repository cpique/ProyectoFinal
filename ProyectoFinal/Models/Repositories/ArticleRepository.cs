using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models.Repositories
{
    public class ArticleRepository : IArticleRepository, IDisposable
    {
        #region Properties
        public GymContext context;
        private bool disposed = false;
        #endregion

        #region Constructors
        public ArticleRepository(GymContext context)
        {
            this.context = context;
        }
        #endregion

        #region Interface Implementation
        public IEnumerable<Article> GetArticles()
        {
            return context.Articles.Include(a => a.Supplier).ToList();
        }

        public Article GetArticleByID(int id)
        {
            return context.Articles.Find(id);
        }

        public void InsertArticle(Article article)
        {
            context.Articles.Add(article);
        }

        public void DeleteArticle(int id)
        {
            Article article = context.Articles.Find(id);
            context.Articles.Remove(article);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateArticle(Article article)
        {
            context.Entry(article).State = EntityState.Modified;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
    }
}