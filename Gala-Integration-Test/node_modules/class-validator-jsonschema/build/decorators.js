"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.getMetadataSchema = exports.JSONSchema = void 0;
require("reflect-metadata");
const SCHEMA_KEY = Symbol('class-validator-jsonschema:JSONSchema');
function JSONSchema(schema) {
    return (target, key) => {
        if (key) {
            setMetadataSchema(schema, target.constructor, key);
        }
        else {
            setMetadataSchema(schema, target, target.name);
        }
    };
}
exports.JSONSchema = JSONSchema;
function getMetadataSchema(target, key) {
    return Reflect.getMetadata(SCHEMA_KEY, target.constructor, key) || {};
}
exports.getMetadataSchema = getMetadataSchema;
function setMetadataSchema(value, target, key) {
    return Reflect.defineMetadata(SCHEMA_KEY, value, target, key);
}
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiZGVjb3JhdG9ycy5qcyIsInNvdXJjZVJvb3QiOiIiLCJzb3VyY2VzIjpbIi4uL3NyYy9kZWNvcmF0b3JzLnRzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiI7OztBQUVBLDRCQUF5QjtBQUl6QixNQUFNLFVBQVUsR0FBRyxNQUFNLENBQUMsdUNBQXVDLENBQUMsQ0FBQTtBQXdCbEUsU0FBZ0IsVUFBVSxDQUFDLE1BQXVCO0lBQ2hELE9BQU8sQ0FBQyxNQUF5QixFQUFFLEdBQVksRUFBRSxFQUFFO1FBQ2pELElBQUksR0FBRyxFQUFFO1lBQ1AsaUJBQWlCLENBQUMsTUFBTSxFQUFFLE1BQU0sQ0FBQyxXQUFXLEVBQUUsR0FBRyxDQUFDLENBQUE7U0FDbkQ7YUFBTTtZQUNMLGlCQUFpQixDQUFDLE1BQU0sRUFBRSxNQUFNLEVBQUcsTUFBbUIsQ0FBQyxJQUFJLENBQUMsQ0FBQTtTQUM3RDtJQUNILENBQUMsQ0FBQTtBQUNILENBQUM7QUFSRCxnQ0FRQztBQUtELFNBQWdCLGlCQUFpQixDQUMvQixNQUF5QixFQUN6QixHQUFXO0lBRVgsT0FBTyxPQUFPLENBQUMsV0FBVyxDQUFDLFVBQVUsRUFBRSxNQUFNLENBQUMsV0FBVyxFQUFFLEdBQUcsQ0FBQyxJQUFJLEVBQUUsQ0FBQTtBQUN2RSxDQUFDO0FBTEQsOENBS0M7QUFLRCxTQUFTLGlCQUFpQixDQUN4QixLQUFzQixFQUN0QixNQUF5QixFQUN6QixHQUFXO0lBRVgsT0FBTyxPQUFPLENBQUMsY0FBYyxDQUFDLFVBQVUsRUFBRSxLQUFLLEVBQUUsTUFBTSxFQUFFLEdBQUcsQ0FBQyxDQUFBO0FBQy9ELENBQUMifQ==