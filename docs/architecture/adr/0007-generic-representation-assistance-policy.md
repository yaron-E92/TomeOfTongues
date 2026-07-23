# ADR 0007: Generic Representation-Assistance Policy

- Status: Accepted
- Date: 2026-07-23

## Context

Rōmaji fading is learning policy, not Japanese presentation logic.

## Decision

Packs declare assistance relationships between representations. A generic
`RepresentationAssistancePolicy` produces visible, revealable, or hidden
decisions for Always, Adaptive, Tap-to-reveal, and Never modes.

Adaptive fading uses configurable independent evidence and hysteresis; it
restores help after repeated mistakes. MAUI renders the decision without
language-specific branches.

## Consequences

The same mechanism can later support pronunciation or transliteration aids in
other languages. Assistance use remains measurable separately from ability.

## Rejected alternatives

- MAUI rōmaji conditionals.
- Permanent fading without restoration.
- Treating transliteration use as failure.
