﻿using AutoMapper;
using BLL.Interfaces;
using UnitsOfWork.Interfaces;
using Entities;
using BLL.Ninject;
using DTO;
using DTO.Hotels;
using Entities.Hotels;
using DTO.Transports;
using Entities.Transports;
using DTO.Files;
using Entities.Files;

namespace BLL.Services
{
    public class TourService : ITourService
    {
        IUnitOfWork UoW;

        public TourService(IUnitOfWork UoW)
        {
            this.UoW = UoW;
        }

        public TourService()
        {
            UoW = DependencyResolver.ResolveUoW();
        }

        IMapper Mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<TourDTO, Tour>();
            cfg.CreateMap<Tour, TourDTO>();
            cfg.CreateMap<HotelDTO, Hotel>();
            cfg.CreateMap<Hotel, HotelDTO>();
            cfg.CreateMap<HotelRoomDTO, HotelRoom>();
            cfg.CreateMap<HotelRoom, HotelRoomDTO>();
            cfg.CreateMap<TransportDTO, Transport>();
            cfg.CreateMap<TransportPlaceDTO, TransportPlace>();
            cfg.CreateMap<Transport, TransportDTO>();
            cfg.CreateMap<TransportPlace, TransportPlaceDTO>();
            cfg.CreateMap<ImageDTO, Image>();
            cfg.CreateMap<Image, ImageDTO>();
        }).CreateMapper();

        public void AddTour(TourDTO NewTour)
        {
            UoW.ToursTemplates.Add(Mapper.Map<TourDTO, Tour>(NewTour));
        }

        public void DeleteTour(int Id)
        {
            UoW.ToursTemplates.Delete(Id);
        }

        public void EditTour(int Id, TourDTO Tour)
        {
            Tour tour = UoW.ToursTemplates.Get(Id);
            tour = Mapper.Map<TourDTO, Tour>(Tour);
            UoW.ToursTemplates.Modify(Id, tour);
        }

        public IEnumerable<TourDTO> GetAllToursTemplates()
        {
            return Mapper
                .Map<IEnumerable<Tour>, IEnumerable<TourDTO>>(UoW
                .ToursTemplates
                .GetAll());
        }

        public IEnumerable<TourDTO> FindTourTemplatesByPrice(IEnumerable<TourDTO> tours, decimal MinPrice, decimal MaxPrice)
        {
            return tours
                .Where(t => 
                t.Price >= MinPrice && 
                t.Price <= MaxPrice);
        }

        public IEnumerable<TourDTO> FindTourTemplates(IEnumerable<TourDTO> tours, string SeachElem)
        {
            return tours
                .Where(t => 
                t.City == SeachElem ||
                t.Country == SeachElem ||
                t.Title == SeachElem);
        }
        public IEnumerable<TourDTO> FindTourTemplatesByType(IEnumerable<TourDTO> tours, string Type)
        {

            return tours
                .Where(t =>
                t.Type == Type);
        }


        public IEnumerable<TourDTO> FindTourTemplatesByCity(IEnumerable<TourDTO> tours, string City)
        {
            return tours
                .Where(t =>
                t.City == City);
        }

        public IEnumerable<TourDTO> FindTourTemplatesByCountry(IEnumerable<TourDTO> tours, string Country)
        {
            return tours
                .Where(t => 
                t.Country == Country);
        }

        public IEnumerable<TourDTO> FindTourTemplatesByDuration(IEnumerable<TourDTO> tours, int MinDuration, int MaxDuration)
        {
            return tours
                .Where(t => 
                t.Duration >= MinDuration && 
                t.Duration <= MaxDuration);
        }

        public IEnumerable<TourDTO> GetAllToursTemplatesOrderedByPrice(IEnumerable<TourDTO> tours = null)
        {
            if(tours == null)
            {
                return GetAllToursTemplates()
                    .OrderBy(t => 
                    t.Price);
            }
            else
            {
                return tours
                    .OrderBy(t => 
                    t.Price);
            }
        }

        public IEnumerable<TourDTO> GetAllToursTemplatesOrderedByDuration(IEnumerable<TourDTO> tours = null)
        {
            if (tours == null)
            {
                return GetAllToursTemplates()
                    .OrderBy(t =>
                    t.Duration);
            }
            else
            {
                return tours
                    .OrderBy(t =>
                    t.Duration);
            }
        }

        public IEnumerable<TourDTO> GetAllToursTemplatesOrderedByCountry(IEnumerable<TourDTO> tours = null)
        {
            if (tours == null)
            {
                return GetAllToursTemplates()
                    .OrderBy(t =>
                    t.Country);
            }
            else
            {
                return tours
                    .OrderBy(t =>
                    t.Country);
            }
        }

        public IEnumerable<TourDTO> GetAllHotTourTemplates(IEnumerable<TourDTO> tours = null)
        {
            if (tours == null)
            {
                return GetAllToursTemplates()
                    .Where(t =>
                    t.IsHotOffer == true);
            }
            else
            {
                return tours
                    .Where(t =>
                    t.IsHotOffer == true);
            }
        }
        public TourDTO GetTour(int Id)
        {
            return Mapper
                .Map<Tour, TourDTO>(UoW
                .ToursTemplates
                .Get(Id));
        }
    }
}
