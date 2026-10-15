# Community Event Scheduler

## Project Description

Community Event Scheduler is an ASP.NET Core MVC web application for the Congolese (DRC) community in Washington State.

The goal of this website is to help event organizers plan their events, check available dates, and avoid scheduling conflicts in the community.

Visitors can view upcoming events for free and search by city, date, or event category.

Event organizers will be able to create an account, check if a date is available, reserve that date, and pay a reservation fee to publish their event.

The website is not for selling tickets or booking seats for event participants. It is for organizing, reserving dates, and advertising community events.

## How the Website Will Work

1. An organizer visits the website to check upcoming community events.
2. The organizer selects a date and location.
3. The system checks for other events scheduled on the same date within a nearby area.
4. If the date is available, the organizer can submit an event reservation.
5. The organizer pays the reservation fee.
6. After confirmation, the event is published on the community calendar.
7. The organizer's invitation or event flyer can also appear on the website as an advertisement.

## Scheduling Conflict Rules

The website will use location and distance to help prevent scheduling conflicts.

We plan to start with a distance of 30 miles between events on the same date.

For example:

- An event in Seattle and another event in Bellevue on the same date may create a conflict because the cities are close.
- An event in Olympia and another event in Kirkland may be allowed on the same date because the cities are farther apart.

The 30-mile distance is our starting rule and may be adjusted after testing.

## Examples of Community Events

- Weddings
- Funerals
- Birthday parties
- Community meetings
- Conferences
- Celebrations
- Religious events
- Other community events

## Current Features

These features have been developed or are being tested:

- User registration and login using ASP.NET Core Identity
- Create, view, edit, and delete events
- Manage event locations
- Manage event categories
- View upcoming community events
- Search and filter events by city, category, and date
- Responsive event pages using Bootstrap

Some features still need additional testing and security improvements.

## Planned Features

- Require login before creating or reserving an event
- Allow organizers to manage only their own events
- Check date availability before booking
- Detect scheduling conflicts using distance between locations
- Reserve available event dates
- Accept payment for event reservations
- Send email confirmation after a successful reservation
- Allow organizers to upload event invitations or flyers
- Display event advertisements on the home page
- Automatically rotate event advertisements using JavaScript
- Admin management for reservations and events

## Technologies

- ASP.NET Core MVC
- C#
- Entity Framework Core
- SQL Server
- ASP.NET Core Identity
- HTML
- CSS
- Bootstrap 5
- JavaScript
- Git and GitHub

## Getting Started

These steps explain how to run the project on another computer.

### Requirements

- Visual Studio with ASP.NET and web development tools
- A compatible .NET SDK
- SQL Server or SQL Server LocalDB
- Entity Framework Core tools

### Installation

**1. Clone the repository**

```bash
git clone https://github.com/fsengele/CommunityEventScheduler.git
```

**2. Open the project**

Open the solution file in Visual Studio.

**3. Configure the database**

Check the SQL Server connection string in `appsettings.json` and make sure it matches your local SQL Server setup.

Do not commit real passwords or other secrets to GitHub.

**4. Apply database migrations**

Open Visual Studio and go to:

Tools → NuGet Package Manager → Package Manager Console

Run:

```powershell
Update-Database
```

This command applies the Entity Framework Core migrations to create or update the database.

**5. Run the application**

Start the project using the HTTPS profile in Visual Studio.

Open the website and test the Home, Events, Register, and Login pages.

**Note:** The setup instructions may need to be updated based on the final .NET version, database connection, and project configuration.

## Project Goal

The goal of Community Event Scheduler is to provide one place where members of the Congolese community in Washington State can see upcoming events before planning their own.

By checking dates and locations, organizers can avoid scheduling major events too close to each other.

The website will also help organizers advertise their events by displaying event information and invitations.

We want to help our community plan better, communicate better, and avoid unnecessary scheduling conflicts.

## Author

Francis Sengele

GitHub: https://github.com/fsengele
