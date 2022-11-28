# Documentation for DTOs and Routing

## Allgemein

Es werden für jedes Objekt, welche in der Datenbank vorhanden sind DTOs erzeugt. Diese verfügen über die gleichen Properties, aber verwenden bei Referenzen auf ein anders Objekt auf der Datenbank nur das dazugehörige DTO. Diese müssen dabei immer gleich heißen wie die jeweils zugehörige Datenbankklasse angehängt mit der **Endung -Dto (Teacher.cs &rarr; TeacherDto.cs)**.

## Routing

> #### students/courses-list
>
> ![students/courses-list](documents\images\students_courses-list.png)

---

> #### students/priority-list
>
> ![students/priority-list](documents\images\students_priority-list.png)

---

> #### admin/search-students
>
> ![admin/search-students](documents\images\admin_search-students.png)

---

## DTOs

### OfferDetailDto

Dieses DTO wird verwendet bei den verschiedenen detailierten Kursanzeigen. (Siehe Bild)

| Name            | Type           | Description                                                                                                                                                         |
| --------------- | -------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| OfferId         | int            | -                                                                                                                                                                   |
| CouresTitle     | string         | -                                                                                                                                                                   |
| Description     | string         | Beschreibung des Kurses                                                                                                                                             |
| TeacherNames    | string[&nbsp;] | Lehrer, welche den Kurs haben. Der Lehrer an erster Stelle ist hauptverantwortlicher                                                                                |
| StartDate       | DateTime       | -                                                                                                                                                                   |
| EndTime         | DateTime       | -                                                                                                                                                                   |
| CurrentStudents | int            | Die aktuelle Anzahl an eingetragener Schüler                                                                                                                        |
| MaximumStudents | int?           | Anzahl an maximal Zulässiger Schüler (ist null wenn kein Maximum gegeben)                                                                                           |
| MinimumStudents | int?           | Anzahl an mindest benötigten Schülern (ist null wenn kein Minimum benötigt)                                                                                         |
| Price           | decimal?       | Kosten des Courses (ist null wenn keine Kosten fällig)                                                                                                              |
| Location        | string         | Veranstaltungsort des Kurses                                                                                                                                        |
| MeetingPoint    | string         | Treffpunkt des Kurses. Treffpunkt des Kurses kann ja Unterschiedlich sein (z.B. Anfahrt zu Location ist mit Bus, Treffpunkt ist aber HTL, da dort der Bus wegfährt) |

![OfferDetailDto](documents\images\offerDetailDto.png)

---

### OfferListDto

> #### 🔴🔴🔴 WARNING 🔴🔴🔴
>
> **OfferDto** gehört noch umbenannt in **OfferListDto**

Dieses DTO wird verwendet bei der Admin Course Übersicht

| Name       | Type               | Description |
| ---------- | ------------------ | ----------- |
| OfferId    | int                | -           |
| Title      | string             | -           |
| OfferDates | List\<OfferDates\> | -           |
| TeacherId  | int                | -           |
| Teacher    | Teacher            | -           |

![OfferListDto](documents\images\offerListDto.png)

---

### OfferCreateDto

> #### 🔴🔴🔴 WARNING 🔴🔴🔴
>
> Bitte um Rückinfo, ob das stimmt

| Name            | Type                 | Description                                                                                                                                                         |
| --------------- | -------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| CouresTitle     | string               | -                                                                                                                                                                   |
| Description     | string               | Beschreibung des Kurses                                                                                                                                             |
| Teachers        | TeacherDto[&nbsp;]   | Lehrer, welche den Kurs haben. Der Lehrer an erster Stelle ist hauptverantwortlicher                                                                                |
| StartDate       | DateTime             | -                                                                                                                                                                   |
| EndTime         | DateTime             | -                                                                                                                                                                   |
| OfferDates      | OfferDateDto[&nbsp;] | -                                                                                                                                                                   |
| Clazzes         | ClazzDto[&nbsp;]     | -                                                                                                                                                                   |
| MaximumStudents | int?                 | Anzahl an maximal Zulässiger Schüler (ist null wenn kein Maximum gegeben)                                                                                           |
| MinimumStudents | int?                 | Anzahl an mindest benötigten Schülern (ist null wenn kein Minimum benötigt)                                                                                         |
| Price           | decimal?             | Kosten des Courses (ist null wenn keine Kosten fällig)                                                                                                              |
| Location        | string               | Veranstaltungsort des Kurses                                                                                                                                        |
| MeetingPoint    | string               | Treffpunkt des Kurses. Treffpunkt des Kurses kann ja Unterschiedlich sein (z.B. Anfahrt zu Location ist mit Bus, Treffpunkt ist aber HTL, da dort der Bus wegfährt) |

---

### StudentsWithOfferDto

| Name           | Type                      | Description |
| -------------- | ------------------------- | ----------- |
| StudentId      | int                       | -           |
| FirstName      | string                    | -           |
| LastName       | string                    | -           |
| ClassName      | string                    | -           |
| SelectedOffers | List<OfferOfStudentsDto\> | -           |

![studentsWithOfferDto](documents\images\admin_students-list.png)

![admin/search-students](documents\images\admin_search-students.png)

---

### OfferOfStudentsDto

| Name      | Type   | Description |
| --------- | ------ | ----------- |
| DateTime  | Date   | -           |
| OfferName | string | -           |

---

### LehrerListDto

> #### 🔴🔴🔴 WARNING 🔴🔴🔴
>
> Habe ich das richtig verstanden mit dem Status? Oder wo genau bekommt man den sonst her?
> **Wird noch geändert, nicht erstellen!**

| Name    | Type               | Description                                                                                             |
| ------- | ------------------ | ------------------------------------------------------------------------------------------------------- |
| Teacher | TeacherDto[&nbsp;] | -                                                                                                       |
| Status  | int                | Status kann Zahl sein zwischen 1 und 3. 1 = alles eingetragen, 2 = teilweise, 3 = gar nicht eingetragen |

![LehrerListDto](documents\images\lehrerList.png)

---

### LehrerDetailDto

> #### 🔴🔴🔴 WARNING 🔴🔴🔴
>
> Bitte überprüfen

| Name       | Type     | Description |
| ---------- | -------- | ----------- |
| CourseName | string   | -           |
| Date       | DateTime | -           |

![LehrerDetailDto](documents\images\lehrerDetailDto.png)

---

### KlassenFilterListDto

> #### 🔴🔴🔴 WARNING 🔴🔴🔴
>
> Student als Objekt übergeben? Wieder die Frage mit Status.

| Name        | Type   | Description                                                                                             |
| ----------- | ------ | ------------------------------------------------------------------------------------------------------- |
| ClassName   | string | -                                                                                                       |
| StudentName | string | -                                                                                                       |
| Status      | int    | Status kann Zahl sein zwischen 1 und 3. 1 = alles eingetragen, 2 = teilweise, 3 = gar nicht eingetragen |

![KlassenFilterList](documents\images\klassenFilterList.png)

---

## Controller und Services

### OfferController & OfferService

> #### 🔴🔴🔴 WARNING 🔴🔴🔴
>
> **OfferDto** gehört noch umbenannt in **OfferListDto**

| Controller | Service | Methodenname         | Returntyp                    | Parameter                   | HttpActionName  | Status |
| :--------: | :-----: | -------------------- | ---------------------------- | --------------------------- | --------------- | :----: |
|     X      |         | GetOffers()          | IEnumerable\<OfferDto\>      | -                           | GetOffers       |   🟢   |
|            |    X    | GetAllOffers()       | IEnumerable\<OfferDto\>      | -                           | -               |   🟢   |
|     X      |         | DeleteOffers()       | ReplyDto                     | id: int                     | DeleteOffer     |   🟢   |
|            |    X    | DeleteOfferById()    | ReplyDto                     | id: int                     | -               |   🟢   |
|     X      |         | UpdateOffer()        | OfferListDto                 | offer: OfferListDto         | UpdateOffer     |   🟢   |
|            |    X    | UpdateOffer()        | OfferListDto                 | dto: OfferListDto           | -               |   🟢   |
|     X      |         | GetOfferDetails()    | IEnumerable<OfferDetailDto\> | studentId: int              | GetDetailOffers |   🔴   |
|            |    X    | GetAllOfferDetails() | IEnumerable<OfferDetailDto\> | studentId: int              | -               |   🔴   |
|     X      |         | CreateNewOffer()     | OfferDetailDto               | createOffer: CreateOfferDto | CreateOffer     |   🔴   |
|            |    X    | CreateNewOffer()     | OfferDetailDto               | createOffer: CreateOfferDto | -               |   🔴   |

---

### LehrerController & LehrerService

| Controller | Service | Methodenname      | Returntyp                      | Parameter     | HttpActionName  | Status |
| :--------: | :-----: | ----------------- | ------------------------------ | ------------- | --------------- | :----: |
|     X      |         | GetLehrerList()   | IEnumerable\<LehrerListDto\>   |               | GetLehrerList   |   🔴   |
|            |    X    | GetLehrerList()   | IEnumerable\<LehrerListDto\>   |               | -               |   🔴   |
|     X      |         | GetLehrerDetail() | IEnumerable\<LehrerDetailDto\> | lehrerID: int | GetLehrerDetail |   🔴   |
|            |    X    | GetLehrerDetail() | IEnumerable\<LehrerDetailDto\> | lehrerID: int | -               |   🔴   |

---

### FreistellungsController & FreistellungsService

| Controller | Service | Methodenname      | Returntyp                        | Parameter                          | HttpActionName | Status |
| :--------: | :-----: | ----------------- | -------------------------------- | ---------------------------------- | -------------- | :----: |
|     X      |         | SetFreistellung() | ActionResult\<FreistellungsDto\> | freistellungsDto: FreistellungsDto | [action]       |   🔴   |
|            |    X    | SetFreistellung() | bool                             | freistellungsDto: FreistellungsDto | -              |   🔴   |

---
