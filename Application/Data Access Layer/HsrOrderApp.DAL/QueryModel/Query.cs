using System;
using System.Collections.Generic;
using System.Text;

namespace HsrOrderApp.DAL.QueryModel
{
    public sealed class Query
    {
        private List<Criterion> criteria = new List<Criterion>();
        private QueryOperator @operator;
        private IList<Query> subQueries = new List<Query>();
        private IList<OrderClause> orderClauses = new List<OrderClause>();

        public IList<Criterion> Criteria
        {
            get
            {
                return criteria;
            }
        }

        public QueryOperator Operator
        {
            get
            {
                return @operator;
            }
            set
            {
                @operator = value;
            }
        }

        public IList<Query> SubQueries
        {
            get
            {
                return subQueries;
            }
        }

        public IList<OrderClause> OrderClauses
        {
            get
            {
                return orderClauses;
            }
        }
    }

    //public sealed class Query
    //{
    //    //private List<Criterion> criteria = new List<Criterion>();
    //    //private QueryOperator @operator;
    //    //private IList<Query> subQueries;

    //    //public IList<Criterion> Criteria
    //    //{
    //    //    get
    //    //    {
    //    //        return criteria;
    //    //    }
    //    //}

    //    //public QueryOperator Operator
    //    //{
    //    //    get
    //    //    {
    //    //        return @operator;
    //    //    }
    //    //    set
    //    //    {
    //    //        @operator = value;
    //    //    }
    //    //}

    //    //public IList<Query> SubQueries
    //    //{
    //    //    get
    //    //    {
    //    //        throw new System.NotImplementedException();
    //    //    }
    //    //    set
    //    //    {
    //    //    }
    //    //}
    //}
}