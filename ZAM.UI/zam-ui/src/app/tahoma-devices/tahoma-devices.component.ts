import { Component } from '@angular/core';
import { TahomaServiceService } from '../services/tahoma-service.service';
import { tahomaDevice } from './interfaces/tahoma-device';

@Component({
  selector: 'app-tahoma-devices',
  templateUrl: './tahoma-devices.component.html',
  styleUrls: ['./tahoma-devices.component.css']
})
export class TahomaDevicesComponent {

  constructor(private tahomaService: TahomaServiceService){  }

  kotlownia: tahomaDevice = {
    label: "kotlownia",
    actionCommand: ["up", "down"]
  };

  kancelaria: tahomaDevice = {
    label: "kancelaria",
    actionCommand: ["left", "right"]
  };

  devices = [
    this.kotlownia,
    this.kancelaria
  ];

  actions!: string[];

  selectedDevice: tahomaDevice | null = null;


  refreshActionsForDevice(){
    console.log(this.selectedDevice);
  }
}


