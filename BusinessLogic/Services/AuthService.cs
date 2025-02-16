using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class AuthService
    {
        private readonly IKullaniciRepository _kullaniciRepo;

        public AuthService(IKullaniciRepository kullaniciRepo)
        {
            _kullaniciRepo = kullaniciRepo;
        }

        // Kullanıcı adı ve parola ile doğrulama
        public async Task<bool> AuthenticateAsync(string kullaniciAdi, string parola)
        {
            var user = await _kullaniciRepo.GetByUsernameAsync(kullaniciAdi);
            return user != null && user.Parola == parola;
        }

        // Yeni kullanıcı kaydı
        public async Task<bool> RegisterAsync(string kullaniciAdi, string parola)
        {
            var existingUser = await _kullaniciRepo.GetByUsernameAsync(kullaniciAdi);
            if (existingUser != null)
                return false; // Kullanıcı zaten var.

            var user = new Kullanici
            {
                KullaniciAdi = kullaniciAdi,
                Parola = parola // Parola burada basitçe veriliyor, gerçek projelerde şifre hashing yapılmalı.
            };

            await _kullaniciRepo.AddAsync(user);
            return true;
        }

        // Parola değiştirme
        public async Task<bool> ChangePasswordAsync(string kullaniciAdi, string eskiParola, string yeniParola)
        {
            var user = await _kullaniciRepo.GetByUsernameAsync(kullaniciAdi);
            if (user == null || user.Parola != eskiParola)
                return false; // Kullanıcı bulunamadı veya eski parola yanlış.

            user.Parola = yeniParola; // Gerçek projelerde parola hashing yapılmalı.
            await _kullaniciRepo.UpdateAsync(user);
            return true;
        }

        // Kullanıcıyı çıkartma (Logout)
        public async Task LogoutAsync(string kullaniciAdi)
        {
            // Eğer session veya token tabanlı bir oturum yönetimi yapılıyorsa burada oturum sonlandırılabilir.
            // Burada örnek olarak oturum sonlandırma işlemi basit tutulmuştur.
            await Task.CompletedTask;
        }

        // Parola güvenlik kontrolü
        public bool ValidatePasswordAsync(string parola)
        {
            // Parola güvenliği için temel kontrol (örneğin, uzunluk, özel karakterler vb.)
            if (parola.Length < 6)
                return false; // Parola en az 6 karakter olmalı

            return true;
        }

        //public async Task<bool> ChangePasswordAsync(string kullaniciAdi, string eskiParola, string yeniParola)
        //{
        //    var user = await _kullaniciRepo.GetByUsernameAsync(kullaniciAdi);
        //    if (user == null || !BCrypt.Net.BCrypt.Verify(eskiParola, user.Parola))
        //        return false; // Kullanıcı bulunamadı veya eski parola yanlış.

        //    // Yeni parolayı hash'leyip kaydet
        //    user.Parola = BCrypt.Net.BCrypt.HashPassword(yeniParola);
        //    await _kullaniciRepo.UpdateAsync(user);
        //    return true;
        //}
    }
}
