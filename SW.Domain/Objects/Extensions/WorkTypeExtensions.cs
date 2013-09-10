using System.Linq;
using Orca.Domain.Cache;

namespace Orca.Domain.Objects.Extensions
{
    public static class WorkTypeExtensions
    {


        public static PropertyValueSearchCriteria GetItemSelectionSearchCriteria(this WorkType workType)
        {
            return new PropertyValueSearchCriteria(workType.ItemSelectionCriteria.SelectionCriterion.Select(item => new PropertyValueSearchCriterion(item.PropertyValueName, null)).ToList());
        }

        //public static List<PropertyValueSearchCriterion> GetPropertyValueSearchCriteria(this WorkInstruction workInstruction)
        //{
        //    var criteria = workInstruction.workType.ItemSelectionCriteria.SelectionCriterion.Select(item => new PropertyValueSearchCriterion(item.Name, item.SelectionCriteria.SelectionCriterion)).ToList();

        //    foreach (PropertyValueSearchCriterion criteria in request.Criteria) //then pull the values for the search criteria and get workinstructions.
        //    {
        //        criteria.PropertyValue = wiItem.GetPropertyValueObject(criteria.Name).Value;
        //    }
        //}-
    }
}
