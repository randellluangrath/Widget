# Widget

The technologies used to build this were the latest versions of Angular and .NET Core. PrimeNG was the material library used for the user interface. The architecture pattern chosen for the back-end is the Onion Architecture which emphasizes the use of interfaces for behavior contracts, and it forces the externalization of infrastructure. Couplings are toward the center.

![Widget](https://user-images.githubusercontent.com/7450751/221445160-a266b098-c8a9-4741-bd46-bfa937979152.png)

![widget-demo](https://user-images.githubusercontent.com/7450751/220909758-5b1f03f8-5527-40d7-845d-01b23936e227.gif)


# API Documentation
```
{
  "openapi": "3.0.1",
  "info": {
    "title": "Widget.Web",
    "version": "1.0"
  },
  "paths": {
    "/v1/Local": {
      "get": {
        "tags": [
          "Local"
        ],
        "parameters": [
          {
            "name": "q",
            "in": "query",
            "schema": {
              "type": "string"
            }
          },
          {
            "name": "pageSize",
            "in": "query",
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success"
          },
          "500": {
            "description": "Server Error"
          }
        }
      }
    },
    "/v1/News": {
      "get": {
        "tags": [
          "News"
        ],
        "parameters": [
          {
            "name": "q",
            "in": "query",
            "schema": {
              "type": "string"
            }
          },
          {
            "name": "from",
            "in": "query",
            "schema": {
              "type": "string",
              "format": "date-time"
            }
          },
          {
            "name": "page",
            "in": "query",
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          },
          {
            "name": "pageSize",
            "in": "query",
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success"
          },
          "404": {
            "description": "Not Found",
            "content": {
              "text/plain": {
                "schema": {
                  "$ref": "#/components/schemas/ProblemDetails"
                }
              },
              "application/json": {
                "schema": {
                  "$ref": "#/components/schemas/ProblemDetails"
                }
              },
              "text/json": {
                "schema": {
                  "$ref": "#/components/schemas/ProblemDetails"
                }
              }
            }
          },
          "500": {
            "description": "Server Error"
          }
        }
      }
    },
    "/v1/Weather": {
      "get": {
        "tags": [
          "Weather"
        ],
        "parameters": [
          {
            "name": "q",
            "in": "query",
            "schema": {
              "type": "string"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "Success"
          },
          "400": {
            "description": "Bad Request",
            "content": {
              "text/plain": {
                "schema": {
                  "$ref": "#/components/schemas/ProblemDetails"
                }
              },
              "application/json": {
                "schema": {
                  "$ref": "#/components/schemas/ProblemDetails"
                }
              },
              "text/json": {
                "schema": {
                  "$ref": "#/components/schemas/ProblemDetails"
                }
              }
            }
          },
          "404": {
            "description": "Not Found",
            "content": {
              "text/plain": {
                "schema": {
                  "$ref": "#/components/schemas/ProblemDetails"
                }
              },
              "application/json": {
                "schema": {
                  "$ref": "#/components/schemas/ProblemDetails"
                }
              },
              "text/json": {
                "schema": {
                  "$ref": "#/components/schemas/ProblemDetails"
                }
              }
            }
          },
          "500": {
            "description": "Server Error"
          }
        }
      }
    }
  },
  "components": {
    "schemas": {
      "ProblemDetails": {
        "type": "object",
        "properties": {
          "type": {
            "type": "string",
            "nullable": true
          },
          "title": {
            "type": "string",
            "nullable": true
          },
          "status": {
            "type": "integer",
            "format": "int32",
            "nullable": true
          },
          "detail": {
            "type": "string",
            "nullable": true
          },
          "instance": {
            "type": "string",
            "nullable": true
          }
        },
        "additionalProperties": { }
      }
    }
  }
}
```

# Get Started

To start the project, you'll do `dotnet run` targeting the IIS Express Run Configuration

TODOs: 
- Add code coverage
- Implement more caching where necessary
- Use DTOs and define model responsibilities
- Clean up front-end architecture, components, theming, etc
