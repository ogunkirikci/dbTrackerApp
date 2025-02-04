# DB Tracker Application

A system for monitoring, recording, and reporting database activities.

## System Architecture

The application consists of the following microservices:

### 1. Config Service (Port: 5001)
- Manages system configurations and rules
- Uses PostgreSQL database
- Provides rule definition and management APIs

### 2. Collector Service (Port: 5002)
- Collects database activities
- Sends collected data to Kafka
- Performs real-time data collection and processing

### 3. Processor Service (Port: 5003)
- Processes data from Kafka
- Stores processed data in Elasticsearch
- Performs data analysis and reporting

## Infrastructure Services

### 1. PostgreSQL
- Port: 5432
- User: postgres
- Password: postgres
- Database: dbtracker

### 2. Kafka & Zookeeper
- Kafka Port: 9092
- Zookeeper Port: 2181
- Used as message queue system

### 3. Elasticsearch
- Port: 9200
- Used for storing and querying audit logs

## Running the Project

### Requirements
- Docker
- Docker Compose

### Installation and Running

1. Clone the project:
```bash
git clone https://github.com/ogunkirikci/dbTrackerApp.git
cd dbTrackerApp
```

2. Start Docker containers:
```bash
docker-compose up --build
```

3. Wait for services to start in the following order:
   - Zookeeper starts first
   - Then Kafka starts
   - PostgreSQL and Elasticsearch start independently
   - Finally, .NET services start

### Checking Services

To check the status of services:
```bash
docker-compose ps
```

### Stopping Services

To stop all services:
```bash
docker-compose down
```

To stop all services and clean volumes:
```bash
docker-compose down -v
```

## API Endpoints

### Config Service (5001)
- `GET /api/rules` - Lists defined rules
- `POST /api/rules` - Adds new rule
- `PUT /api/rules/{id}` - Updates rule
- `DELETE /api/rules/{id}` - Deletes rule

### Collector Service (5002)
- `POST /api/collect` - Records database activity
- `GET /api/status` - Checks service status

### Processor Service (5003)
- `GET /api/audit-events` - Lists audit logs
- `GET /api/audit-events/{id}` - Gets specific audit log
- `GET /api/reports` - Lists reports

## Development

### Project Structure
```
dbTrackerApp/
├── src/
│   ├── DbTracker.ConfigService/
│   ├── DbTracker.CollectorService/
│   ├── DbTracker.ProcessorService/
│   └── DbTracker.Shared/
├── docker-compose.yml
├── .gitignore
└── README.md
```

### Technology Stack
- .NET 8
- PostgreSQL
- Apache Kafka
- Elasticsearch
- Docker & Docker Compose
