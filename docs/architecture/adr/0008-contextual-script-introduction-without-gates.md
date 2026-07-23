# ADR 0008: Contextual Script Introduction Without Gatekeeping

- Status: Accepted
- Date: 2026-07-23

## Context

The Japanese course must introduce kana and kanji gradually without front-loaded
memorization or handwriting requirements.

## Decision

The Japanese pack introduces scripts through practical expressions and declares
representation/grapheme targets. Script mistakes emit targeted evidence and
review work but never lock lessons, units, or the course. Kanji appears with
contextual readings and meanings. Handwriting is outside the MVP.

## Consequences

Curriculum progression and script familiarity remain separate. The generic
engine tracks pack-defined representations without Japanese domain types.

## Rejected alternatives

- Mandatory kana boot camp.
- Script mastery prerequisite.
- Generic `Kana` or `Kanji` Core types.
