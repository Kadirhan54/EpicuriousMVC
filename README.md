# Epicurious

## Proje Açıklaması
Bu proje, yemek tarifleri paylaşma ve keşfetme amacıyla oluşturulmuş bir web uygulamasıdır. Kullanıcılar, kendi yemek tariflerini ekleyebilir, diğer kullanıcıların tariflerini inceleyebilir.

## Projeye Eklenen Özellikler

### 1. Kullanıcı Kayıt ve Giriş
Kullanıcılar, web uygulamasına kayıt olabilir ve giriş yapabilirler. Kayıt olduktan sonra kullanıcılara bir doğrulama emaili gönderilir.

### 2. Yemek Tarifi Ekleme
Kullanıcılar, kendi yemek tariflerini sisteme ekleyebilirler. Her tarif, başlık, açıklama, malzemeler ve yorum gibi temel bilgiler içerir.

### 3. Yorum Yapma
Kullanıcılar, herhangi bir yemek tarifine yorum yapabilirler. Yorumlar, tarif sayfasında görüntülenir.

### 4. Favori Tarifleri Kaydetme
Kullanıcılar, beğendikleri tarifleri favorilere ekleyebilirler. Favori tarifler, kullanıcının profilinde görüntülenebilir.

### 5. Eklenen Tarifi Silme veya Düzenleme
Kullanıcılar kendi ekledikleri yemek tariflerini düzenleyebilir veya silebilir.

## Projeye Eklenen Özellikler (Detaylı)

### 1. Kullanıcı Kayıt ve Giriş
Kullanıcı kaydı için ASP.NET Core Identity kullanılmıştır. Yeni bir kullanıcı kaydı yapıldığında, kullanıcıya doğrulama emaili gönderilir. Kullanıcı girişi için Identity'nin `SignInManager`'ı kullanılmıştır.

### 2. Yemek Tarifi Ekleme
Yemek tarifi ekleme işlemi, `RecipeController`'da gerçekleşir. Kullanıcı, tarif bilgilerini doldurduktan sonra, bu bilgilerle yeni bir `Recipe` nesnesi oluşturulur ve veritabanına eklenir.

### 3. Yorum Yapma
Yorum yapma işlemi, `Comment` entity'si kullanılarak yapılmıştır. Her yorum, ilişkili olduğu yemek tarifiyle ilişkilendirilir. Yorumlar, tarif sayfasında görüntülenir.

### 4. Favori Tarifleri Kaydetme
Favori tarifleri kaydetme, kullanıcının profil sayfasında gerçekleşir. Kullanıcılar, her tarif sayfasında bulunan "Favorilere Ekle" butonu ile favori tariflerini kaydedebilirler. Favori tarifler, veritabanında `SavedRecipes` entity'si ile ilişkilendirilir.

## Görev Dağılımı


## Yaşadığımız Problemler

