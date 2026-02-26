using System.Text.Json;

namespace PlexTools.API;

public class MetadataDeserializer
{
    // Add Deserialize method for returning metadata object.
    public List<Metadata> Deserialize(string json)
    {
        return JsonSerializer.Deserialize<Root>(json)?.MediaContainer?.Metadata ?? new();
    }
}


// Class for the root-layer of the JSON. This is our starting point for the deserialization.
public class Root
{
    // The root-layer contains exactly one MediaContainer.
    public MediaContainer? MediaContainer { get; set; }
}

// MediaContainer found in Root-layer of JSON.
public class MediaContainer
{
    // Every movie or show or whatnot has its metadata contained in a separate metadata-layer.
    // We want every single one of these, hence a list.
    public List<Metadata>? Metadata { get; set; }
}

// Metadata class for containing metadata of our movies and shows and whatnot.
// This is that good preem gas-station weed we need.
public class Metadata
{
    public string? ratingKey { get; set; }
    public string? key { get; set; }
    public string? guid { get; set; }
    public string? slug { get; set; }
    public string? editionTitle { get; set; }
    public string? studio { get; set; }
    public string? type { get; set; }
    public string? title { get; set; }
    public string? librarySectionTitle { get; set; }
    public int? librarySectionID { get; set; }
    public string? librarySectionKey { get; set; }
    public string? contentRating { get; set; }
    public int? contentRatingAge { get; set; }
    public string? summary { get; set; }
    public float? rating { get; set; }
    public float? audienceRating { get; set; }
    public int? year { get; set; }
    public string? tagline { get; set; }
    public string? thumb { get; set; }
    public string? art { get; set; }
    public int? duration { get; set; }
    public DateTime? originallyAvailableAt { get; set; }
    public int? addedAt { get; set; }
    public int? updatedAt { get; set; }
    public string? audienceRatingImage { get; set; }
    public string? chapterSource { get; set; }
    public string? primaryExtraKey { get; set; }
    public string? ratingImage { get; set; }
}