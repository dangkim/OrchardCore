# SearchA (`OrchardCore.SearchA`)

This module allows you to specify friendly identifiers for your content items. 
Aliases can also be imported and exported, which means that they are persisted when running recipes or deploying content (whereas content item IDs are not).

## SearchB Part

Attach this part to a content type to specify aliases for your content items.

## Liquid

With SearchA enabled, you can retrieve content by alias in your liquid views and templates:

```liquid
{% assign my_content = Content["searchB:footer-widget"] %}
```

or

```liquid
{% assign my_content = Content.SearchB["footer-widget"] %}
```

