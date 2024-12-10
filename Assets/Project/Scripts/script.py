import xml.etree.ElementTree as ET
from datetime import datetime
import re
import os
import csv

def format_iso_string(iso_string):
    """
    Adjusts the Apple Watch's timezone offset format in a given datetime string to ensure
    compatibility with the datetime library for proper parsing and interpretation.
    """
    pattern = r"([+-]\d{2})(\d{2})$"
    formatted_string = re.sub(pattern, r"\1:\2", iso_string.strip())
    return formatted_string

def remove_timezone_offset(iso_string):
    """
    Removes timezone offset from an ISO 8601 datetime string.
    """
    dt = datetime.fromisoformat(iso_string)
    return dt.replace(tzinfo=None).isoformat()

def find_relevant_records(xml_file, start, end, type):
    """
    Finds all Record tags whose 'startDate' and 'endDate' attributes are included within
    the timespan between 'start' and 'end', and whose 'type' attribute is the same as
    the input 'type'.
    """
    tree = ET.parse(xml_file)
    root = tree.getroot()
    
    start_dt = datetime.fromisoformat(start)
    end_dt = datetime.fromisoformat(end)

    records = []
    
    for record in root.findall('.//Record'):
        recordType = record.get('type')
        if (recordType != type):
            continue

        start_date = format_iso_string(record.get('startDate'))
        end_date = format_iso_string(record.get('endDate'))

        if start_date and end_date:
            record_start_dt = datetime.fromisoformat(start_date)
            record_end_dt = datetime.fromisoformat(end_date)
            
            if start_dt <= record_start_dt and end_dt >= record_end_dt:
                records.append(record)

    return records

def main():
    xml_file = "export.xml"

    start = "2024-12-10 11:04:00 +0000"
    end = "2024-12-10 11:06:00 +0000"

    heartBeatType = "HKQuantityTypeIdentifierHeartRate"
    energyBurnedType = "HKQuantityTypeIdentifierActiveEnergyBurned"

    # TODO: change this according to workout type we use for testing
    # it probably shouldn't be stair climbing but something like indoor walking
    workoutSummaryType = "HKWorkoutActivityTypeStairClimbing"

    heartbeat_records = find_relevant_records(xml_file, start, end, heartBeatType)
    heartbeat_data = [[r.get('startDate'), r.get('endDate'), r.get('value')] for r in heartbeat_records]

    energy_records = find_relevant_records(xml_file, start, end, energyBurnedType)
    energy_data = [[r.get('startDate'), r.get('endDate'), r.get('value')] for r in energy_records]

    folder_name = "biometrics_results_" \
        + start.replace(" ", "_") \
            .replace(":", "") \
            .replace("+", "") \
        + "_" \
        + end.replace(" ", "_") \
            .replace(":", "") \
            .replace("+", "")
    os.makedirs(folder_name, exist_ok=True)

    with open(f'{folder_name}/heartbeat.csv', mode='w', newline='') as file:
        writer = csv.writer(file)
        writer.writerows(heartbeat_data)

    with open(f'{folder_name}/energy.csv', mode='w', newline='') as file:
        writer = csv.writer(file)
        writer.writerows(energy_data)

if __name__ == "__main__": 
    main() 
