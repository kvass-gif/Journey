using Journey.Data;
using Journey.Entities;

namespace Journey.ViewModels.LandLord
{
    public class LandLordViewCreator
    {
        private readonly UnitOfWork unitOfWork;
        public LandLordViewCreator(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        private Dictionary<int, string> citiesList()
        {
            var cities = unitOfWork.CityRepo.Cities();
            var citySelectedList = new Dictionary<int, string>();
            foreach (var item in cities)
            {
                citySelectedList.Add(item.Id, item.CityName);
            }
            return citySelectedList;
        }
        private Dictionary<int, string> placeTypeList()
        {
            var objs = unitOfWork.TypeRepo.Types();
            var citySelectedList = new Dictionary<int, string>();
            foreach (var item in objs)
            {
                citySelectedList.Add(item.Id, item.TypeName);
            }
            return citySelectedList;
        }
        public Place GetViewModel()
        {
            var viewModel = new Place();
            viewModel.Cities = citiesList();
            viewModel.PlaceTypes = placeTypeList();
            return viewModel;
        }
    }
}
