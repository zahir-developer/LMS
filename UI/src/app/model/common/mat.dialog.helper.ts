import { Injectable } from "@angular/core";

export interface ConfirmDialog
{
  title: string;
  description: string;
  data: number;
}

export interface MatDialogConfig {
  width: string,
  enterAnimationDuration: string,
  exitAnimationDuration: string,
  data: any
}

@Injectable()
export class MatDialogHelper {
  public getMatDialogConfig(data: any) {
    const config: MatDialogConfig = {
      width: '350px',
      enterAnimationDuration: '0ms',
      exitAnimationDuration: '0ms',
      data: data
    }

    return config;
  }

}


