/**
 * LSWBackend
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: 1.0
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */


export interface CreateOfferDto { 
    title?: string | null;
    description?: string | null;
    price?: number | null;
    startDates?: Array<string> | null;
    endDates?: Array<string> | null;
    maxCount?: number;
    minCount?: number;
    location?: string | null;
    meetingPoint?: string | null;
    clazzes?: Array<string> | null;
}

