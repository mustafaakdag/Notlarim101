﻿using System;
using Notlarim101.DataAccessLayer.EntityFramework;
using Notlarim101.Entity;
using Notlarim101.Entity.Messages;
using Notlarim101.Entity.ValueObject;
using Notlarim101.Common.Helper;
using Notlarim101.BusinessLayer.Abstract;

namespace Notlarim101.BusinessLayer
{
    
    public class NotlarimUserManager: ManagerBase<NotlarimUser>
    {
        BusinessLayerResult<NotlarimUser> res = new BusinessLayerResult<NotlarimUser>();
        //Kullanici username kontrolu yapmaliyim
        //kullnici email kontrolu yapmaliyim
        //Kayit islemini gerceklestirmeliyim
        //Activasyon e-postasi gonderimi

        public BusinessLayerResult<NotlarimUser> RegisterUser(RegisterViewModel data)
        {
            NotlarimUser user = Find(s => s.Username == data.Username || s.Email == data.Email);

            if (user!=null)
            {
                if (user.Username==data.Username)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExist, "Kullanici adi kayitli");
                }

                if (user.Email==data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailalreadyExist, "Email kayitli");
                }
                //throw new Exception("Kayitli kullanici yada e-posta adresi");
            }
            else
            {
                int dbResult = Insert(new NotlarimUser()
                {
                    Name = data.Name,
                    Surname = data.Surname,
                    Username = data.Username,
                    Email = data.Email,
                    Password = data.Password,
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = false,
                    IsAdmin = false,
                    //repository e tasindi
                    //ModifiedOn = DateTime.Now,
                    //CreatedOn = DateTime.Now,
                    //ModifiedUsername = "system"
                });
                if (dbResult>0)
                {
                    res.Result =Find(s => s.Email == data.Email && s.Username == data.Username);
                    string siteUri = ConfingHelper.Get<string>("SiteRootUri");
                    string ActivateUri = $"{siteUri}/Home/UserActivate/ { res.Result.ActivateGuid}";
                    string body= $"Merhaba {res.Result.Username};<br><br> Hesabınızı aktifleştimek için <a href='{ActivateUri}' target='_blank'>Tıklayın/a>";

                     MailHelper.SendMail(body, res.Result.Email, "Notlarım101 hesap aktifleştirme");
            
                    //activasyon mail i atilacak
                    //activasyon mail i atilacak
                    //lr.Result.ActivateGuid;
                }
            }
            
            return res;
        }

        public BusinessLayerResult<NotlarimUser> LoginUser(LoginViewModel data)
        {
            //Giris kontrolu
            //Hesap aktif edilmismi kontrolu
            res.Result =Find(s => s.Username == data.Username && s.Password == data.Password);
            if (res.Result!=null)
            {
                if (!res.Result.IsActive)
                {
                    res.AddError(ErrorMessageCode.UserIsNotActive, "Kullanici adi aktiflestirilmemis!!!");
                    res.AddError(ErrorMessageCode.CheckYourEmail, "Lutfen Mailinizi kontrol edin...");
                }
            }
            else
            {
                res.AddError(ErrorMessageCode.UsernameOrPasswordWrong, "kullanici adi yada sifre uyusmuyor.");
            }

            return res;
        }
        public BusinessLayerResult<NotlarimUser> ActivateUser(Guid id)
        {
            res.Result =Find(x => x.ActivateGuid == id);
            if (res.Result!=null)
            {
                if (res.Result.IsActive)
                {
                    res.AddError(ErrorMessageCode.UserAlreadyActive, "Bu hesap daha önce aktif edilmiştir");
                    return res;
                }
                res.Result.IsActive = true;
                Update(res.Result);
            }
            else
            {
                res.AddError(ErrorMessageCode.ActivateIdDoesNotExist, "Muhammed yine mi sen");
            }
            return res;
        }

        public BusinessLayerResult<NotlarimUser> GetUserById(int id)
        {

            res.Result =Find(s => s.Id == id);
            if (res.Result==null)
            {
                res.AddError(ErrorMessageCode.UserNotFound, "Kullanıcı bulunamadı.");
            }
            return res;
        }

        public BusinessLayerResult<NotlarimUser> UpdateProfile(NotlarimUser data)
        {
            NotlarimUser user =Find(s => s.Id != data.Id && (s.Username == data.Username || s.Email == data.Email));
            if (user!=null && user.Id!=data.Id)
            {
                if (user.Username==data.Username)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExist, "Bu kullanıcı adı daha önce kaydedilmiştir");
                }
                if (user.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExist, "Bu E-mail adı daha önce kaydedilmiştir");
                }
                return res;
            }
            res.Result =Find(s => s.Id == data.Id);
            res.Result.Email = data.Email;
            res.Result.Name = data.Name;
            res.Result.Surname = data.Surname;
            res.Result.Password = data.Password;
            res.Result.Username = data.Username;
            if (!string.IsNullOrEmpty(data.ProfileImageFileName))
            {
                res.Result.ProfileImageFileName = data.ProfileImageFileName;
            }
            if (Update(res.Result) == 0)
            {
                res.AddError(ErrorMessageCode.ProfileCouldNotUpdate, "Profil Güncellenemedi");
            }
            return res;
        }

        public BusinessLayerResult<NotlarimUser> RemoveUserById(int id)
        {
            NotlarimUser user =Find(s => s.Id==id);
            if (user!=null)
            {
                if (Delete(user)==0)
                {
                    res.AddError(ErrorMessageCode.UserCouldNotRemove, "Kullanıcı silenemedi");
                }
            }
            else
            {
                res.AddError(ErrorMessageCode.UserCouldNotFind, "Kullanıcı Bulunamadı");
            }
            return res;
        }
    }
}
