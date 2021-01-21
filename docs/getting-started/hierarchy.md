---
title: Components Overview
sidebar_label: Components Overview
---

import Image from '@theme/IdealImage';

It is _important_ to know how components are named and what's their role in the visual tree of the editor in order to understand the code and the documentation.

## Hierarchy and terminology

From a user's perspective the root component is always an [Editor](/docs/components/editor) which holds [Nodes](/docs/components/nodes) and [Connections](/docs/components/connections) together with a few additional UI elements such as a [Selection Rectangle](/docs/components/editor#selection) and a [Pending Connection](/docs/components/editor#pending-connection) in order to make the editor interactive.

Nodes are containers for [Connectors](/docs/components/connectors) or the node itself can be a connector (e.g. [State Node](/docs/components/nodes#state)).

Connectors can create pending connections which can become real connections when completed.

_A picture is worth a thousand words_

<Image img={require('../assets/hierarchy.png')} />

## Items and Connections Layers

You may wonder how can a node be a subclass of a connector and share the same behavior with a node that's not a connector. Actually the editor contains two big layers which helps solving this problem:

1. The items layer - here each node is wrapped inside an [Item Container](/docs/components/item-container) making it possible to have any control representing a node (e.g a connector).
2. The connections layer - this is where all the connections coexist and is rendered behind the items layer by default

Having those two layers separated enables the possibility of [asynchronously loading](/docs/getting-started/async-loading) each one of them.
