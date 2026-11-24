
SELECT
    Id,
    LEN(Organizer) AS OrganizerLength,
    LEN(Description) AS DescriptionLength
FROM Events
WHERE LEN(Organizer) > 50
   OR LEN(Description) > 500;
