# High-Frequency Data Processing API with GC Control

This project demonstrates a high-frequency data processing API built in C# and ASP.NET Core. It simulates a real-world scenario where large volumes of data are processed rapidly, and memory management is critical to avoid latency spikes.

By implementing **garbage collection (GC) control** techniques, we can efficiently manage memory and optimize performance under high memory pressure. The project includes:
- Object pooling to minimize memory allocations.
- GC control using a custom `GcTriggerService` to monitor and trigger GC based on memory usage.

## Features

- **Data Processing API**: Accepts incoming data and processes it with efficient memory management.
- **Memory Optimization with Object Pooling**: Reuses `DataItem` objects to reduce allocations.
- **Adaptive GC Control**: A background service (`GcTriggerService`) monitors memory usage and triggers GC based on a configurable threshold.

## Key Components

### `DataItemPool`
A custom object pool implementation for reusing `DataItem` objects. This reduces the need to frequently allocate and deallocate memory, which helps keep GC cycles minimal and memory usage stable.

### `GcTriggerService`
A background service that periodically checks memory usage and triggers garbage collection if the memory exceeds a specified threshold. This approach provides adaptive memory management, making the application more efficient by preventing unnecessary GC cycles during low memory usage.

### `DataProcessingController`
An API controller that processes incoming data and uses the object pool and GC control to optimize performance:
- **POST /api/DataProcessing/process**: Processes incoming data and adds it to the active data list.
- **POST /api/DataProcessing/clear**: Clears processed data and returns items to the pool, freeing up memory.
