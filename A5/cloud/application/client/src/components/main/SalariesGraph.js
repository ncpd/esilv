import React from "react";
import { Line } from "react-chartjs-2";

/**
 * Component representing Line graph for user salaries
 */
export default class SalariesGraph extends React.Component {

  render() {

    const { salaries } = this.props;

    const salariesData = {
      labels: salaries.timeline,
      datasets: [
        {
          label: 'Salary',
          fill: true,
          lineTension: 0.15,
          backgroundColor: 'rgba(245,148,24,0.4)',
          borderColor: 'rgba(245,148,24,1)',
          borderCapStyle: 'butt',
          borderDash: [],
          borderDashOffset: 0.0,
          borderJoinStyle: 'miter',
          pointBorderColor: 'rgba(245,148,24,1)',
          pointBackgroundColor: '#fff',
          pointBorderWidth: 1,
          pointHoverRadius: 5,
          pointHoverBackgroundColor: 'rgba(245,148,24,1)',
          pointHoverBorderColor: 'rgba(220,220,220,1)',
          pointHoverBorderWidth: 2,
          pointRadius: 1,
          pointHitRadius: 10,
          data: salaries.salaries
        }
      ]
    };
    return (
        <div>
          <Line data={salariesData} />
        </div>
    );
  }
}
