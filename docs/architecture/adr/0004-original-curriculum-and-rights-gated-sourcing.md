# ADR 0004: Original Curriculum and Rights-Gated Sourcing

- Status: Accepted
- Date: 2026-07-23

## Context

Free access does not grant redistribution rights, and an empty engine is not a
usable learning product.

## Decision

The starter Japanese course is authored originally for TomeOfTongues and
linguistically reviewed. It contains six practical lessons and at least twenty
rights-cleared human audio prompts. Original lesson text and audio default to
CC BY-SA 4.0, separately from GPL-3.0 code.

Every bundled asset requires provenance, attribution, license,
redistribution/modification flags, date, checksum, and review status. The pack
compiler rejects incomplete rights records. Protected external courses remain
link-only unless exact terms permit redistribution.

## Consequences

Content authoring and review are MVP work and a release gate. User imports are
private and non-redistributable by default.

## Rejected alternatives

- Scraping or adapting a free online course.
- Bundling third-party audio without explicit permission.
- Shipping a shiny shell without substantive material.
