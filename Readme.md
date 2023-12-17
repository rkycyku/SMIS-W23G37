# SMIS-W23G37

## Rreth Projektit

Ky projekt eshte punuar per projektin ne **Hyrje në Ueb Programim** & **Zhvillimi dhe Dizajnimi i Ueb**.

Ky sistem do te sherbej per menaxhimin e Notave, Pagesave, Orareve etj. te Studenteve.

Sistemi do kete role te ndryshme si: Administrator, Student, Profesor, Puntor Administrate etj.

Sistemi do te permbaj funksione te ndryshme si Paraqitja e Transkriptes, Vertetimin Studentor, Pagesat e Studentit, Paraqitjen e Provimeve, Menaxhimin e Lendeve, Menaxhimin e Studenteve, Krijimin e llogarive, Orarin e ligjeratave, si dhe shume funksione te tjera te ndryshme disa prej ketyre funksioneve veqse jane tashme funksionale.

Ky projekt eshte i punuar ne

- React JS - Frontend
- ASP.NET Core Web App (MVC) - Backend & Fronetend
- MSSQL - Database

Eshte punuar nga:

- **Rilind Kyçyku** - 212257449 (rk57449@ubt-uni.net)
- **Valdrin Dalloshi** - 212261697 (vd616972@ubt-uni.net)
- **Ilire Jezerci** - 212260094 (ij60094@ubt-uni.net)
  
Profesoret:

- **Xhelal Jashari** - can. PhD.
- **Edmond Jajaga** - Assoc. Prof. Dr.

## Konfigurimi

Se pari duhet te behet konfigurimi i Connection String ne W23G37/appsettings.json dhe duhet te nderrohet emri i Server me ate te serverit tuaj, Emri i Databazes nuk preferohet te ndryshohet, pastaj ju duhet te beni run komanden **Update-Database** ne **Serverin e Projektit - W23G37** -> *Tools > NuGet Package Manager > Package Manager Console* e cili do te mundesoj gjenerimin ne teresi te databases dhe insertimin e te dhenave bazike, pasi te keni perfunduar me keto hapa ju duhet qe te beni **run** serverin dhe pastaj ne VSC pjesen e React qe gjendet tek **w23g37web** duhet te hapet ne terminal pastaj duhen te behen run keto komonda: 

- **npm i** - Bene instalimin automatik te paketave te nevojtura,
- **npm run build** - Bene Build Projektin,
*Keto duhen te behen vetem ne qoftese e keni hapur projketin per here te pare*
- **npm start** - Bene startimin e projektit (*Kjo duhet te behet gjithmone kur startojme projektin e React*).

Pasi qe te behet konfigurimi ju mund te kyqeni me keto te dhena:

| **Email** | **Password** | **Aksesi**                    |
| ------------ | ------------ | ----------------------------- |
| admin@ubt-uni.net       | Admin1@        | Administrator (Akses i Plote) |
