let graph = document.querySelector('.base-lines');

function showCoordLines(min_F, max_F, total_W) {

    let vertLine = document.querySelector('.vert-point-line');
    let horLine = document.querySelector('.hor-point-line');

    let leftOffset = graph.getBoundingClientRect().left;
    let rightOffset = graph.getBoundingClientRect().top;

    let currentFr = 0;

    graph.addEventListener('mouseover', (e) => {

        vertLine.style.display = 'block';
        horLine.style.display = 'block';
    });

    graph.addEventListener('mousemove', (e) => {

        vertLine.style.left = e.clientX - leftOffset + 'px';
        horLine.style.top = e.clientY - rightOffset + 'px';

        currentFr = min_F + (e.clientX - leftOffset) * ((max_F - min_F) / total_W);

        if (currentFr > 0) {
            document.querySelector('.info-value-current').innerHTML = currentFr.toFixed(2);
        }
    });

    graph.addEventListener('mouseout', (e) => {

        vertLine.style.display = 'none';
        horLine.style.display = 'none';
    });
}

function setDataToTable(signal) {

    let freq = signal.getAttribute('f')
    document.querySelector('.info-value-freq').innerHTML = parseFloat(freq) / 1000;

    let band = signal.getAttribute('band')
    document.querySelector('.info-value-band').innerHTML = parseFloat(band);

    let level = signal.getAttribute('p')
    document.querySelector('.info-value-level').innerHTML = parseFloat(level).toFixed(2);

    let range = signal.getAttribute('range')
    document.querySelector('.info-value-range').innerHTML = range;

    let polar = signal.getAttribute('polar')
    document.querySelector('.info-value-polar').innerHTML = polar;

    let freql = signal.getAttribute('freql')
    document.querySelector('.info-value-freql').innerHTML = freql;

    let snos = signal.getAttribute('snos')
    document.querySelector('.info-value-snos').innerHTML = snos;

    let dop_mod = signal.getAttribute('dop-mod')
    document.querySelector('.info-value-dop-mod').innerHTML = dop_mod;

    let puk = signal.getAttribute('puk')
    document.querySelector('.info-value-puk').innerHTML = puk;

    let pudk = signal.getAttribute('pudk')
    document.querySelector('.info-value-pudk').innerHTML = pudk;

    let cc = signal.getAttribute('cc')
    document.querySelector('.info-value-cc').innerHTML = cc;

    let pc = signal.getAttribute('pc')
    document.querySelector('.info-value-pc').innerHTML = pc;

    let ac = signal.getAttribute('ac')
    document.querySelector('.info-value-ac').innerHTML = ac;

    let ibs = signal.getAttribute('ibs')
    document.querySelector('.info-value-ibs').innerHTML = ibs;

    let idr = signal.getAttribute('idr')
    document.querySelector('.info-value-idr').innerHTML = idr;

    let syncr = signal.getAttribute('syncr')
    document.querySelector('.info-value-syncr').innerHTML = syncr;

    let dcme = signal.getAttribute('dcme')
    document.querySelector('.info-value-dcme').innerHTML = dcme;

    let oks = signal.getAttribute('oks')
    document.querySelector('.info-value-oks').innerHTML = oks;

    let from = signal.getAttribute('from')
    document.querySelector('.info-value-from').innerHTML = from;

    let to = signal.getAttribute('to')
    document.querySelector('.info-value-to').innerHTML = to;

    let protocols = signal.getAttribute('protocols')
    document.querySelector('.info-value-protocols').innerHTML = protocols;

    let ip = signal.getAttribute('ip')
    document.querySelector('.info-value-ip').innerHTML = ip;

    let info = signal.getAttribute('info')
    document.querySelector('.info-value-info').innerHTML = info;

    let comment = signal.getAttribute('comment')
    document.querySelector('.info-value-comment').innerHTML = comment;

    let accept = signal.getAttribute('accept')
    document.querySelector('.info-value-accept').innerHTML = accept;

    let date = signal.getAttribute('date')
    document.querySelector('.info-value-date').innerHTML = date;

    let operator = signal.getAttribute('operator')
    document.querySelector('.info-value-operator').innerHTML = operator;

    let meanValue = parseInt(signal.getAttribute('mean'));

    let meanName;

    if (meanValue === 111) {
        meanName = "NEW SIG";
    }
    else if (meanValue === 1) {
        meanName = "DORAZV";
    }
    else if (meanValue === 2) {
        meanName = "CLOSED";
    }
    else if (meanValue === 3) {
        meanName = "INF";
    }
    else if (meanValue === 4) {
        meanName = "NSH";
    }
    else if (meanValue === 5) {
        meanName = "OA";
    }
    else if (meanValue === 6) {
        meanName = "PI";
    }
    else if (meanValue === 7) {
        meanName = "TB";
    }
    else if (meanValue === 8) {
        meanName = "XX";
    }
    else {
        meanName = "";
    }

    document.querySelector('.info-value-mean').innerText = meanName;

    let mod = signal.getAttribute('mod')
    if (mod === '')
    {
        document.querySelector('.info-value-mod').innerHTML = 'none';
    }
    else
    {
        document.querySelector('.info-value-mod').innerHTML = mod;
    }

    let graph = document.querySelector('.graph-container');
    let table = document.querySelector('.signal-info');

    if ((signal.getBoundingClientRect().left + signal.clientWidth / 2) < graph.clientWidth / 2) {

        table.classList.add('signal-info-right');

        if (table.classList.contains('signal-info-left')) {

            table.classList.remove('signal-info-left')
        }
    }
    else {

        table.classList.add('signal-info-left');

        if (table.classList.contains('signal-info-right')) {

            table.classList.remove('signal-info-right')
        }
    }
}

function initLines(verticalSteps, horizontalSteps, minFreq, maxFreq){

    let commonWidth = graph.clientWidth;

    let w = commonWidth / verticalSteps;

    for (var j = 1; j < verticalSteps; j++) {

        let verticalLine = document.createElement('div');

        verticalLine.classList.add('vl');

        verticalLine.classList.add('l');

        verticalLine.style.right = j * w + 'px';


        let vertStepValue = parseInt((maxFreq - minFreq) / verticalSteps);

        let verticalValue = document.createElement('div');

        verticalValue.classList.add('vv');

        verticalValue.classList.add('v');

        verticalValue.innerText = j * vertStepValue + minFreq;
        
        verticalValue.style.left = j * w - 10 + 'px';

        

        graph.appendChild(verticalLine);

        graph.appendChild(verticalValue);
    }

    let commonHeight = graph.clientHeight;

    let h = commonHeight / horizontalSteps

    for (let i = 1; i < horizontalSteps; i++) {

        let horizontalLine = document.createElement('div');

        horizontalLine.classList.add('hl');

        horizontalLine.classList.add('l');

        horizontalLine.style.bottom = i * h + 'px';



        let horValue = document.createElement('div');

        horValue.classList.add('hv');

        horValue.classList.add('v');

        horValue.innerText = (i - 1) * 2 + 2;

        horValue.style.bottom = i * h - 10 + 'px';



        graph.appendChild(horValue);

        graph.appendChild(horizontalLine);
    }

    let randomizer = 1;

    document.querySelectorAll('.val').forEach(v => {
        let f = parseFloat(v.getAttribute('f')) / 1000 * commonWidth / (maxFreq - minFreq) - minFreq * commonWidth / (maxFreq - minFreq);

        let p_value = parseInt(v.getAttribute('p'));

        let p = p_value === 0 ? h * 5 : commonHeight / 30 * p_value;

        randomizer *= -1;

        p += Math.random() * h / 2 * randomizer;

        v.style.zIndex = 9999 - parseInt(p);

        let b_val = parseFloat(v.getAttribute('band'));

        let b = b_val / 1000 * commonWidth / (maxFreq - minFreq);

        let mod = parseInt(v.getAttribute('mean'));

        if (mod === 111) {
            v.style.backgroundColor = 'white';
        }
        else if (mod === 1) {
            v.style.backgroundColor = '#1ce41c';
        }
        else if (mod === 2) {
            v.style.backgroundColor = 'gray';
        }
        else if (mod === 3) {
            v.style.backgroundColor = '#da6a08';
        }
        else if (mod === 4) {
            v.style.backgroundColor = '#6de1d4';
        }
        else if (mod === 5) {
            v.style.backgroundColor = '#2c37e4';
        }
        else if (mod === 6) {
            v.style.backgroundColor = 'red';
        }
        else if (mod === 7) {
            v.style.backgroundColor = '#f0fd0e';
        }
        else if (mod === 8) {
            v.style.backgroundColor = '#c10df5';
        }
        else
        {
            v.style.backgroundColor = '#134f13';
        }

        v.style.left = f - b / 2 + 'px';
        v.style.width = b + 'px';
        v.style.height = p + 'px';

        v.addEventListener('click', () => {

            document.querySelector('.signal-info').style.display = 'flex';

            setDataToTable(v);
        });
    });


    showCoordLines(minFreq, maxFreq, commonWidth);

}

let minFreqVal = parseInt(document.querySelector('.min-fr').value);
let maxFreqVal = parseInt(document.querySelector('.max-fr').value);

initLines(20, 15, minFreqVal, maxFreqVal);

window.addEventListener('resize', () => {

    document.querySelectorAll('.l').forEach(l => {
        l.remove();
    });

    document.querySelectorAll('.v').forEach(v => {
        v.remove();
    })

    initLines(15, 15, minFreqVal, maxFreqVal);
});

showCoordLines();

document.body.addEventListener('click', (ev) => {

    if (!ev.target.classList.contains('val')) {
        document.querySelector('.signal-info').style.display = 'none';
    }
});

document.querySelector('.plus-min').addEventListener('click', () => {

    let minFreqInput = document.querySelector('.min-fr');

    let currentVal = parseInt(minFreqInput.value);

    minFreqInput.value = currentVal + 100;

});

document.querySelector('.minus-min').addEventListener('click', () => {

    let minFreqInput = document.querySelector('.min-fr');

    let currentVal = parseInt(minFreqInput.value);

    if (currentVal >= 100) {
        minFreqInput.value = currentVal - 100;
    }

});

document.querySelector('.plus-max').addEventListener('click', () => {

    let minFreqInput = document.querySelector('.max-fr');

    let currentVal = parseInt(minFreqInput.value);

    minFreqInput.value = currentVal + 100;

});

document.querySelector('.minus-max').addEventListener('click', () => {

    let minFreqInput = document.querySelector('.max-fr');

    let currentVal = parseInt(minFreqInput.value);

    if (currentVal >= 100) {
        minFreqInput.value = currentVal - 100;
    }

});

function swichUnaccepted(swicher) {

    let signals = Array.from(document.querySelectorAll('.val'));

    if (swicher === 'false') {

        document.querySelector('.swich-road').style.transform = 'translate(40px)';

        signals.forEach(s => {
            s.style.display = 'block';
        });

        document.querySelector('.swich').setAttribute('on', 'true');
    }
    else if (swicher === 'true') {
        document.querySelector('.swich-road').style.transform = 'translate(0px)';

        signals.forEach(s => {
            switchNumber = parseInt(s.getAttribute('swich'));

            if (switchNumber === 0) {

                s.style.display = 'none';
            }
        });

        document.querySelector('.swich').setAttribute('on', 'false');
    }

}

let swicherValue = localStorage.getItem('on');

if (swicherValue !== '') {
    swichUnaccepted(swicherValue);
}

document.querySelector('.swich-head').addEventListener('click', () => {

    let isOn = document.querySelector('.swich').getAttribute('on');

    swichUnaccepted(isOn);

    localStorage.setItem('on', isOn);

});