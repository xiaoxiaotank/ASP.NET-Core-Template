import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';


@Component({
  selector: 'workspace',
  templateUrl: './workspace.component.html',
  styleUrls: ['./workspace.component.scss']
})
export class WorkspaceComponent implements OnInit {

  isCollapsed = false;
  subLayoutStyle: object;
  logoTextStyle: object;
  triggerTemplate: TemplateRef<void> | null = null;
  @ViewChild('trigger') customTrigger: TemplateRef<void>;

  constructor() { }

  ngOnInit() {
    this.setSubLayoutStyle();
    this.setLogoStyle();
  }

  /** custom trigger can be TemplateRef **/
  changeTrigger(): void {
    this.triggerTemplate = this.customTrigger;
  }

  setCollapsed(): void {
    this.isCollapsed = !this.isCollapsed;
    this.setSubLayoutStyle();
    this.setLogoStyle();
  }

  setSubLayoutStyle(): void {
    if (this.isCollapsed) {
      this.subLayoutStyle = {
        'margin-left': '80px'
      };
    } else {
      this.subLayoutStyle = {
        'margin-left': '200px'
      };
    }
  }

  setLogoStyle(): void {
    if (this.isCollapsed) {
      this.logoTextStyle = {
        'display': 'none',
        'margin-left': '0',
      }
    } else {
      this.logoTextStyle = {
        'display': 'inline-block',
        'margin-left': '10px',
      }
    }
  }
}
