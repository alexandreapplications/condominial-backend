using AlexandreApps.Condominial.Backend.Interfaces.AppService;
using AlexandreApps.Condominial.Backend.Interfaces.AppService.Global;
using AlexandreApps.Condominial.Backend.Interfaces.DataService.Global;
using AlexandreApps.Condominial.Backend.Model.Global;
using AlexandreApps.Condominial.Backend.Model.Global.ViewModel;
using AlexandreApps.Condominial.Backend.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexandreApps.Condominial.Backend.Appservice.Global
{
    public class PersonAppService : IPersonAppService, ICrudAppService<PersonViewModel>
    {
        private IPersonDataService _personDataService;

        public PersonAppService(IPersonDataService personDataService)
        {
            this._personDataService = personDataService;
        }
        public async Task<IEnumerable<PersonViewModel>> Get(Guid id)
        {
            var answer = await this._personDataService.Get(id);

            return await Task.FromResult(this.Map(answer));
        }

        public async Task<IEnumerable<PersonViewModel>> GetAll()
        {
            var answer = await this._personDataService.GetAll();

            return await Task.FromResult(this.Map(answer));
        }

        public async Task<IEnumerable<Guid>> Insert(UserModel user, params PersonViewModel[] models)
        {
            var addedRecord = new List<PersonModel>();

            foreach (var item in models)
            {
                var newId = Guid.NewGuid();
                var addressId = Guid.NewGuid();
                var contactsId = Guid.NewGuid();
                var notesId = Guid.NewGuid();
                var now = DateTime.UtcNow;
                addedRecord.Add(new PersonModel
                {
                    Id = newId,
                    IsActive = item.IsActive,
                    Name = item.Name,
                    DocumentCode = item.DocumentCode,
                    DocumentType = item.DocumentType,
                    PreferedLanguage = item.PreferedLanguage,
                    Addresses = item.Addresses.Select(y => new AddressModel
                    {
                        Id = addressId,
                        Street = y.Street,
                        Number = y.Number,
                        ComplementaryInformation = y.ComplementaryInformation,
                        City = y.City,
                        Province = y.Province,
                        Country = y.Country,
                        Latitude = y.Latitude,
                        Longitude = y.Longitude,
                        ZipCode = y.ZipCode,
                        IncludeDate = now,
                        IsActive = true,
                        LastUser = Guid.Empty,
                        UpdateDate = null
                    }).ToArray(),
                    Contacts = item.Contacts.Select(y => new ContactModel
                    {
                        Id = contactsId,
                        Type = y.Type,
                        Value = y.Value,
                        Comment = y.Comment,
                        IncludeDate = now,
                        IsActive = true,
                        LastUser = Guid.Empty,
                        UpdateDate = null
                    }).ToArray(),
                    Notes = item.Notes.Select(y => new NoteModel
                    {
                        Id = notesId,
                        Note = y.Text,
                        IncludeDate = now,
                        LastUser = Guid.Empty,
                        UpdateDate = null
                    }).ToArray()
                });
            }
            return await this._personDataService.Insert(addedRecord);
        }

        public async Task<IEnumerable<Guid>> Update(UserModel user, params PersonViewModel[] models)
        {
            var recordsToUpdate = new List<PersonModel>();
            var now = DateTime.UtcNow;

            foreach (var model in models)
            {
                var currentRecord = await _personDataService.Get(model.Id);
                var record = currentRecord.First();
                record.Name = model.Name;
                record.DocumentCode = model.DocumentCode;
                record.DocumentType = model.DocumentType;
                record.PreferedLanguage = model.PreferedLanguage;
                record.IsActive = model.IsActive;
                foreach (var item in model.Addresses)
                {
                    if ((item.Id ?? Guid.Empty) == Guid.Empty)
                    {
                        item.Id = Guid.NewGuid();
                    }
                    var current = record.Addresses.SingleOrDefault(x => x.Id == item.Id);
                    if (current == null)
                    {
                        var newGuid = Guid.NewGuid();
                        record.Addresses.Add(new AddressModel
                        {
                            Id = current.Id,
                            Street = current.Street,
                            Number = current.Number,
                            ComplementaryInformation = current.ComplementaryInformation,
                            City = current.City,
                            Province = current.Province,
                            Country = current.Country,
                            ZipCode = current.ZipCode,
                            Longitude = current.Longitude,
                            Latitude = current.Latitude,
                            IncludeDate = now,
                            IsActive = true
                        });
                    }
                    else
                    {
                        current.Id = current.Id;
                        current.Street = current.Street;
                        current.Number = current.Number;
                        current.ComplementaryInformation = current.ComplementaryInformation;
                        current.City = current.City;
                        current.Province = current.Province;
                        current.Country = current.Country;
                        current.ZipCode = current.ZipCode;
                        current.Longitude = current.Longitude;
                        current.Latitude = current.Latitude;
                        current.UpdateDate = now;
                    }
                }
                foreach (var item in model.Contacts)
                {
                    if ((item.Id ?? Guid.Empty) == Guid.Empty)
                    {
                        item.Id = Guid.NewGuid();
                    }
                    var current = record.Contacts.SingleOrDefault(x => x.Id == item.Id);

                    if (current == null)
                    {
                        var newGuid = Guid.NewGuid();
                        record.Contacts.Add(new ContactModel
                        {
                            Id = item.Id.Value,
                            Type = item.Type,
                            Comment = item.Comment,
                            Value = item.Value,
                            IncludeDate = now,
                            IsActive = true
                        });
                    }
                    else
                    {
                        current.Type = item.Type;
                        current.Comment = item.Comment;
                        current.Value = item.Value;
                        current.UpdateDate = now;
                    }
                }
                foreach (var item in model.Notes)
                {
                    if ((item.Id ?? Guid.Empty) == Guid.Empty)
                    {
                        item.Id = Guid.NewGuid();
                    }
                    var current = record.Notes.SingleOrDefault(x => x.Id == item.Id);

                    if (current == null)
                    {
                        var newGuid = Guid.NewGuid();
                        record.Notes.Add(new NoteModel
                        {
                            Id = item.Id.Value,
                            Note = item.Text,
                            UpdateDate = now
                        });
                    }
                    else
                    {
                        current.Note = item.Text;
                        current.IncludeDate = now;
                    }
                }
            }

            return await _personDataService.Update(recordsToUpdate);
        }

        public async Task<IEnumerable<Guid>> Delete(UserModel user, params Guid[] ids)
        {
            return await this._personDataService.Delete(ids);
        }

        public IList<PersonViewModel> Map(IList<PersonModel> models)
        {
            return (from x in models
                    select new PersonViewModel
                    {
                        Id = x.Id,
                        IsActive = x.IsActive,
                        Name = x.Name,
                        DocumentCode = x.DocumentCode,
                        DocumentType = x.DocumentType,
                        PreferedLanguage = x.PreferedLanguage,
                        Addresses = x.Addresses.Select(y => new AddressViewModel
                        {
                            Id = y.Id,
                            Street = y.Street,
                            Number = y.Number,
                            ComplementaryInformation = y.ComplementaryInformation,
                            City = y.City,
                            Province = y.Province,
                            Country = y.Country,
                            Latitude = y.Latitude,
                            Longitude = y.Longitude,
                            ZipCode = y.ZipCode
                        }).ToArray(),
                        Contacts = x.Contacts.Select(y => new ContactViewModel
                        {
                            Id = y.Id,
                            Type = y.Type,
                            Value = y.Value,
                            Comment = y.Comment
                        }).ToArray(),
                        Notes = x.Notes.Select(y => new NoteViewModel
                        {
                            Id = y.Id,
                            Text = y.Note,
                            LastUpdate = y.UpdateDate ?? y.IncludeDate
                        }).ToArray(),
                        LastUpdate = x.UpdateDate ?? x.IncludeDate
                    }).ToArray();
        }
    }
}
