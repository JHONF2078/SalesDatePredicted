import { CommonModule } from '@angular/common';
import { Component, AfterViewInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import * as d3 from 'd3';

@Component({
  selector: 'app-d3-grapic',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './d3-grapic.component.html',
  styleUrl: './d3-grapic.component.css'
})
export class D3GrapicComponent implements AfterViewInit {

  inputValue: string = '4,8,15,16';
  errorMessage: string = '';
  maxValue: number = 100;

  ngAfterViewInit() {
    this.drawChart([4, 8, 15, 16]);
  }

  updateChartFromInput(input: string) {
    this.errorMessage = '';

    const rawValues = input.split(',').map(val => val.trim());

    const data = rawValues
      .map(val => parseInt(val, 10))
      .filter(val => !isNaN(val) && val >= 0 && val <= this.maxValue);

    const invalids = rawValues.filter(val => {
      const num = parseInt(val, 10);
      return isNaN(num) || num < 0 || num > this.maxValue;
    });

    if (invalids.length > 0) {
      this.errorMessage = `Los siguientes valores no son válidos: ${invalids.join(', ')}`;
      return;
    }

    d3.select('#chart').selectAll('*').remove();
    this.drawChart(data);
  }

  drawChart(data: number[]) {
    const width = 600;
    const barHeight = 30;

    // Crear escala lineal
    const xScale = d3.scaleLinear()
      .domain([0, d3.max(data) || 1])
      .range([0, width - 20]);

    const svg = d3.select('#chart')
      .append('svg')
      .attr('width', width)
      .attr('height', barHeight * data.length);

    const color = d3.scaleOrdinal(d3.schemeTableau10);

    const bar = svg.selectAll('g')
      .data(data)
      .enter().append('g')
      .attr('transform', (_, i) => `translate(0,${i * barHeight})`);

    bar.append('rect')
      .attr('width', d => xScale(d))
      .attr('height', barHeight - 1)
      .attr('fill', (_, i) => color(i.toString()));

    bar.append('text')
      .attr('x', d => xScale(d) - 5)
      .attr('y', barHeight / 2)
      .attr('dy', '.35em')
      .attr('fill', 'white')
      .attr('text-anchor', 'end')
      .text(d => d.toString());
  }

}
