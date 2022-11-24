# Documentation for DTOs and Routing

## Allgemein

Es werden für jedes Objekt, welche in der Datenbank vorhanden sind DTOs erzeugt. Diese verfügen über die gleichen Properties, aber verwenden bei Referenzen auf ein anders Objekt auf der Datenbank nur das dazugehörige DTO. Diese müssen dabei immer gleich heißen wie die jeweils zugehörige Datenbankklasse angehängt mit der **Endung -Dto (Teacher.cs &rarr; TeacherDto.cs)**.

## Routing

## DTOs

### OfferDetailDto

Dieses DTO wird verwendet bei den verschiedenen detailierten Kursanzeigen. (Siehe Bild)

![OfferDetailDto](documents\images\offerDetailDto.png)

| Name            | Type           | Description                                                                                                                                                                                                  |
| --------------- | -------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| CouresTitle     | string         | -                                                                                                                                                                                                            |
| Description     | string         | Beschreibung des Kurses                                                                                                                                                                                      |
| TeacherNames    | string[&nbsp;] | Lehrer, welche den Kurs haben. Der Lehrer an erster Stelle ist hauptverantwortlicher                                                                                                                         |
| StartDate       | DateTime       | -                                                                                                                                                                                                            |
| EndTime         | DateTime       | -                                                                                                                                                                                                            |
| CurrentStudents | int            | Die aktuelle Anzahl an eingetragener Schüler                                                                                                                                                                 |
| MaximumStudents | int?           | Anzahl an maximal Zulässiger Schüler (ist null wenn kein Maximum gegeben)                                                                                                                                    |
| MinimumStudents | int?           | Anzahl an mindest benötigten Schülern (ist null wenn kein Minimum benötigt)                                                                                                                                  |
| Price           | decimal?       | Kosten des Courses (ist null wenn keine Kosten fällig)                                                                                                                                                       |
| Location        | string         | Veranstaltungsort des Kurses                                                                                                                                                                                 |
| MeetingPoint    | string?        | Treffpunkt des Kurses. Ist null wenn es keinen Treffpunkt gibt. Treffpunkt des Kurses kann ja Unterschiedlich sein (z.B. Anfahrt zu Location ist mit Bus, Treffpunkt ist aber HTL, da dort der Bus wegfährt) |

### OfferListDto

Dieses DTO wird verwendet bei der Admin Course Übersicht

![OfferListDto](documents\images\offerListDto.png)

## Controller und Services
