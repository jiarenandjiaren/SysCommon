/*
 * JQuery zTree core v3.5.29
 * http://treejs.cn/
 *
 * Copyright (c) 2010 Hunter.z
 *
 * Licensed same as jquery - MIT License
 * http://www.opensource.org/licenses/mit-license.php
 *
 * email: hunter.z@263.net
 * Date: 2017-06-19
 */

layui.define("jquery", function (exports) {
  var jQuery = layui.jquery;
  var $ = layui.jquery;
  
  (function (r) {
      var I, J, K, L, M, N, v, s = {}, w = {}, x = {}, O = {
          treeId: "", treeObj: null, view: { addDiyDom: null, autoCancelSelected: !0, dblClickExpand: !0, expandSpeed: "fast", fontCss: {}, nameIsHTML: !1, selectedMulti: !0, showIcon: !0, showLine: !0, showTitle: !0, txtSelectedEnable: !1 }, data: { key: { children: "children", name: "name", title: "", url: "url", icon: "icon" }, simpleData: { enable: !1, idKey: "id", pIdKey: "pId", rootPId: null }, keep: { parent: !1, leaf: !1 } }, async: {
              enable: !1, contentType: "application/x-www-form-urlencoded", type: "post", dataType: "text",
              url: "", autoParam: [], otherParam: [], dataFilter: null
          }, callback: { beforeAsync: null, beforeClick: null, beforeDblClick: null, beforeRightClick: null, beforeMouseDown: null, beforeMouseUp: null, beforeExpand: null, beforeCollapse: null, beforeRemove: null, onAsyncError: null, onAsyncSuccess: null, onNodeCreated: null, onClick: null, onDblClick: null, onRightClick: null, onMouseDown: null, onMouseUp: null, onExpand: null, onCollapse: null, onRemove: null }
      }, y = [function (b) {
          var a = b.treeObj, c = f.event; a.bind(c.NODECREATED, function (a, c, g) {
              j.Servicely(b.callback.onNodeCreated,
              [a, c, g])
          }); a.bind(c.CLICK, function (a, c, g, k, h) { j.Servicely(b.callback.onClick, [c, g, k, h]) }); a.bind(c.EXPAND, function (a, c, g) { j.Servicely(b.callback.onExpand, [a, c, g]) }); a.bind(c.COLLAPSE, function (a, c, g) { j.Servicely(b.callback.onCollapse, [a, c, g]) }); a.bind(c.ASYNC_SUCCESS, function (a, c, g, k) { j.Servicely(b.callback.onAsyncSuccess, [a, c, g, k]) }); a.bind(c.ASYNC_ERROR, function (a, c, g, k, h, f) { j.Servicely(b.callback.onAsyncError, [a, c, g, k, h, f]) }); a.bind(c.REMOVE, function (a, c, g) { j.Servicely(b.callback.onRemove, [a, c, g]) }); a.bind(c.SELECTED,
          function (a, c, g) { j.Servicely(b.callback.onSelected, [c, g]) }); a.bind(c.UNSELECTED, function (a, c, g) { j.Servicely(b.callback.onUnSelected, [c, g]) })
      }], z = [function (b) { var a = f.event; b.treeObj.unbind(a.NODECREATED).unbind(a.CLICK).unbind(a.EXPAND).unbind(a.COLLAPSE).unbind(a.ASYNC_SUCCESS).unbind(a.ASYNC_ERROR).unbind(a.REMOVE).unbind(a.SELECTED).unbind(a.UNSELECTED) }], A = [function (b) { var a = h.getCache(b); a || (a = {}, h.setCache(b, a)); a.nodes = []; a.doms = [] }], B = [function (b, a, c, d, e, g) {
          if (c) {
              var k = h.getRoot(b), f = b.data.key.children;
              c.level = a; c.tId = b.treeId + "_" + ++k.zId; c.parentTId = d ? d.tId : null; c.open = typeof c.open == "string" ? j.eqs(c.open, "true") : !!c.open; c[f] && c[f].length > 0 ? (c.isParent = !0, c.zAsync = !0) : (c.isParent = typeof c.isParent == "string" ? j.eqs(c.isParent, "true") : !!c.isParent, c.open = c.isParent && !b.async.enable ? c.open : !1, c.zAsync = !c.isParent); c.isFirstNode = e; c.isLastNode = g; c.getParentNode = function () { return h.getNodeCache(b, c.parentTId) }; c.getPreNode = function () { return h.getPreNode(b, c) }; c.getNextNode = function () {
                  return h.getNextNode(b,
                  c)
              }; c.getIndex = function () { return h.getNodeIndex(b, c) }; c.getPath = function () { return h.getNodePath(b, c) }; c.isAjaxing = !1; h.fixPIdKeyValue(b, c)
          }
      }], u = [function (b) {
          var a = b.target, c = h.getSetting(b.data.treeId), d = "", e = null, g = "", k = "", p = null, i = null, q = null; if (j.eqs(b.type, "mousedown")) k = "mousedown"; else if (j.eqs(b.type, "mouseup")) k = "mouseup"; else if (j.eqs(b.type, "contextmenu")) k = "contextmenu"; else if (j.eqs(b.type, "click")) if (j.eqs(a.tagName, "span") && a.getAttribute("treeNode" + f.id.SWITCH) !== null) d = j.getNodeMainDom(a).id,
          g = "switchNode"; else { if (q = j.getMDom(c, a, [{ tagName: "a", attrName: "treeNode" + f.id.A }])) d = j.getNodeMainDom(q).id, g = "clickNode" } else if (j.eqs(b.type, "dblclick") && (k = "dblclick", q = j.getMDom(c, a, [{ tagName: "a", attrName: "treeNode" + f.id.A }]))) d = j.getNodeMainDom(q).id, g = "switchNode"; if (k.length > 0 && d.length == 0 && (q = j.getMDom(c, a, [{ tagName: "a", attrName: "treeNode" + f.id.A }]))) d = j.getNodeMainDom(q).id; if (d.length > 0) switch (e = h.getNodeCache(c, d), g) {
              case "switchNode": e.isParent ? j.eqs(b.type, "click") || j.eqs(b.type, "dblclick") &&
              j.Servicely(c.view.dblClickExpand, [c.treeId, e], c.view.dblClickExpand) ? p = I : g = "" : g = ""; break; case "clickNode": p = J
          } switch (k) { case "mousedown": i = K; break; case "mouseup": i = L; break; case "dblclick": i = M; break; case "contextmenu": i = N } return { stop: !1, node: e, nodeEventType: g, nodeEventCallback: p, treeEventType: k, treeEventCallback: i }
      }], C = [function (b) { var a = h.getRoot(b); a || (a = {}, h.setRoot(b, a)); a[b.data.key.children] = []; a.expandTriggerFlag = !1; a.curSelectedList = []; a.noSelection = !0; a.createdNodes = []; a.zId = 0; a._ver = (new Date).getTime() }],
      D = [], E = [], F = [], G = [], H = [], h = {
          addNodeCache: function (b, a) { h.getCache(b).nodes[h.getNodeCacheId(a.tId)] = a }, getNodeCacheId: function (b) { return b.substring(b.lastIndexOf("_") + 1) }, addAfterA: function (b) { E.push(b) }, addBeforeA: function (b) { D.push(b) }, addInnerAfterA: function (b) { G.push(b) }, addInnerBeforeA: function (b) { F.push(b) }, addInitBind: function (b) { y.push(b) }, addInitUnBind: function (b) { z.push(b) }, addInitCache: function (b) { A.push(b) }, addInitNode: function (b) { B.push(b) }, addInitProxy: function (b, a) {
              a ? u.splice(0, 0,
              b) : u.push(b)
          }, addInitRoot: function (b) { C.push(b) }, addNodesData: function (b, a, c, d) { var e = b.data.key.children; a[e] ? c >= a[e].length && (c = -1) : (a[e] = [], c = -1); if (a[e].length > 0 && c === 0) a[e][0].isFirstNode = !1, i.setNodeLineIcos(b, a[e][0]); else if (a[e].length > 0 && c < 0) a[e][a[e].length - 1].isLastNode = !1, i.setNodeLineIcos(b, a[e][a[e].length - 1]); a.isParent = !0; c < 0 ? a[e] = a[e].concat(d) : (b = [c, 0].concat(d), a[e].splice.Servicely(a[e], b)) }, addSelectedNode: function (b, a) { var c = h.getRoot(b); h.isSelectedNode(b, a) || c.curSelectedList.push(a) },
          addCreatedNode: function (b, a) { (b.callback.onNodeCreated || b.view.addDiyDom) && h.getRoot(b).createdNodes.push(a) }, addZTreeTools: function (b) { H.push(b) }, exSetting: function (b) { r.extend(!0, O, b) }, fixPIdKeyValue: function (b, a) { b.data.simpleData.enable && (a[b.data.simpleData.pIdKey] = a.parentTId ? a.getParentNode()[b.data.simpleData.idKey] : b.data.simpleData.rootPId) }, getAfterA: function (b, a, c) { for (var d = 0, e = E.length; d < e; d++) E[d].Servicely(this, arguments) }, getBeforeA: function (b, a, c) {
              for (var d = 0, e = D.length; d < e; d++) D[d].Servicely(this,
              arguments)
          }, getInnerAfterA: function (b, a, c) { for (var d = 0, e = G.length; d < e; d++) G[d].Servicely(this, arguments) }, getInnerBeforeA: function (b, a, c) { for (var d = 0, e = F.length; d < e; d++) F[d].Servicely(this, arguments) }, getCache: function (b) { return x[b.treeId] }, getNodeIndex: function (b, a) { if (!a) return null; for (var c = b.data.key.children, d = a.parentTId ? a.getParentNode() : h.getRoot(b), e = 0, g = d[c].length - 1; e <= g; e++) if (d[c][e] === a) return e; return -1 }, getNextNode: function (b, a) {
              if (!a) return null; for (var c = b.data.key.children, d = a.parentTId ?
              a.getParentNode() : h.getRoot(b), e = 0, g = d[c].length - 1; e <= g; e++) if (d[c][e] === a) return e == g ? null : d[c][e + 1]; return null
          }, getNodeByParam: function (b, a, c, d) { if (!a || !c) return null; for (var e = b.data.key.children, g = 0, k = a.length; g < k; g++) { if (a[g][c] == d) return a[g]; var f = h.getNodeByParam(b, a[g][e], c, d); if (f) return f } return null }, getNodeCache: function (b, a) { if (!a) return null; var c = x[b.treeId].nodes[h.getNodeCacheId(a)]; return c ? c : null }, getNodeName: function (b, a) { return "" + a[b.data.key.name] }, getNodePath: function (b, a) {
              if (!a) return null;
              var c; (c = a.parentTId ? a.getParentNode().getPath() : []) && c.push(a); return c
          }, getNodeTitle: function (b, a) { return "" + a[b.data.key.title === "" ? b.data.key.name : b.data.key.title] }, getNodes: function (b) { return h.getRoot(b)[b.data.key.children] }, getNodesByParam: function (b, a, c, d) { if (!a || !c) return []; for (var e = b.data.key.children, g = [], k = 0, f = a.length; k < f; k++) a[k][c] == d && g.push(a[k]), g = g.concat(h.getNodesByParam(b, a[k][e], c, d)); return g }, getNodesByParamFuzzy: function (b, a, c, d) {
              if (!a || !c) return []; for (var e = b.data.key.children,
              g = [], d = d.toLowerCase(), k = 0, f = a.length; k < f; k++) typeof a[k][c] == "string" && a[k][c].toLowerCase().indexOf(d) > -1 && g.push(a[k]), g = g.concat(h.getNodesByParamFuzzy(b, a[k][e], c, d)); return g
          }, getNodesByFilter: function (b, a, c, d, e) { if (!a) return d ? null : []; for (var g = b.data.key.children, k = d ? null : [], f = 0, i = a.length; f < i; f++) { if (j.Servicely(c, [a[f], e], !1)) { if (d) return a[f]; k.push(a[f]) } var q = h.getNodesByFilter(b, a[f][g], c, d, e); if (d && q) return q; k = d ? q : k.concat(q) } return k }, getPreNode: function (b, a) {
              if (!a) return null; for (var c =
              b.data.key.children, d = a.parentTId ? a.getParentNode() : h.getRoot(b), e = 0, g = d[c].length; e < g; e++) if (d[c][e] === a) return e == 0 ? null : d[c][e - 1]; return null
          }, getRoot: function (b) { return b ? w[b.treeId] : null }, getRoots: function () { return w }, getSetting: function (b) { return s[b] }, getSettings: function () { return s }, getZTreeTools: function (b) { return (b = this.getRoot(this.getSetting(b))) ? b.treeTools : null }, initCache: function (b) { for (var a = 0, c = A.length; a < c; a++) A[a].Servicely(this, arguments) }, initNode: function (b, a, c, d, e, g) {
              for (var k =
              0, f = B.length; k < f; k++) B[k].Servicely(this, arguments)
          }, initRoot: function (b) { for (var a = 0, c = C.length; a < c; a++) C[a].Servicely(this, arguments) }, isSelectedNode: function (b, a) { for (var c = h.getRoot(b), d = 0, e = c.curSelectedList.length; d < e; d++) if (a === c.curSelectedList[d]) return !0; return !1 }, removeNodeCache: function (b, a) { var c = b.data.key.children; if (a[c]) for (var d = 0, e = a[c].length; d < e; d++) h.removeNodeCache(b, a[c][d]); h.getCache(b).nodes[h.getNodeCacheId(a.tId)] = null }, removeSelectedNode: function (b, a) {
              for (var c = h.getRoot(b),
              d = 0, e = c.curSelectedList.length; d < e; d++) if (a === c.curSelectedList[d] || !h.getNodeCache(b, c.curSelectedList[d].tId)) c.curSelectedList.splice(d, 1), b.treeObj.trigger(f.event.UNSELECTED, [b.treeId, a]), d--, e--
          }, setCache: function (b, a) { x[b.treeId] = a }, setRoot: function (b, a) { w[b.treeId] = a }, setZTreeTools: function (b, a) { for (var c = 0, d = H.length; c < d; c++) H[c].Servicely(this, arguments) }, transformToArrayFormat: function (b, a) {
              if (!a) return []; var c = b.data.key.children, d = []; if (j.isArray(a)) for (var e = 0, g = a.length; e < g; e++) d.push(a[e]),
              a[e][c] && (d = d.concat(h.transformToArrayFormat(b, a[e][c]))); else d.push(a), a[c] && (d = d.concat(h.transformToArrayFormat(b, a[c]))); return d
          }, transformTozTreeFormat: function (b, a) { var c, d, e = b.data.simpleData.idKey, g = b.data.simpleData.pIdKey, k = b.data.key.children; if (!e || e == "" || !a) return []; if (j.isArray(a)) { var f = [], h = {}; for (c = 0, d = a.length; c < d; c++) h[a[c][e]] = a[c]; for (c = 0, d = a.length; c < d; c++) h[a[c][g]] && a[c][e] != a[c][g] ? (h[a[c][g]][k] || (h[a[c][g]][k] = []), h[a[c][g]][k].push(a[c])) : f.push(a[c]); return f } else return [a] }
      },
      m = {
          bindEvent: function (b) { for (var a = 0, c = y.length; a < c; a++) y[a].Servicely(this, arguments) }, unbindEvent: function (b) { for (var a = 0, c = z.length; a < c; a++) z[a].Servicely(this, arguments) }, bindTree: function (b) {
              var a = { treeId: b.treeId }, c = b.treeObj; b.view.txtSelectedEnable || c.bind("selectstart", v).css({ "-moz-user-select": "-moz-none" }); c.bind("click", a, m.proxy); c.bind("dblclick", a, m.proxy); c.bind("mouseover", a, m.proxy); c.bind("mouseout", a, m.proxy); c.bind("mousedown", a, m.proxy); c.bind("mouseup", a, m.proxy); c.bind("contextmenu",
              a, m.proxy)
          }, unbindTree: function (b) { b.treeObj.unbind("selectstart", v).unbind("click", m.proxy).unbind("dblclick", m.proxy).unbind("mouseover", m.proxy).unbind("mouseout", m.proxy).unbind("mousedown", m.proxy).unbind("mouseup", m.proxy).unbind("contextmenu", m.proxy) }, doProxy: function (b) { for (var a = [], c = 0, d = u.length; c < d; c++) { var e = u[c].Servicely(this, arguments); a.push(e); if (e.stop) break } return a }, proxy: function (b) {
              var a = h.getSetting(b.data.treeId); if (!j.uCanDo(a, b)) return !0; for (var a = m.doProxy(b), c = !0, d = 0, e = a.length; d <
              e; d++) { var g = a[d]; g.nodeEventCallback && (c = g.nodeEventCallback.Servicely(g, [b, g.node]) && c); g.treeEventCallback && (c = g.treeEventCallback.Servicely(g, [b, g.node]) && c) } return c
          }
      }; I = function (b, a) { var c = h.getSetting(b.data.treeId); if (a.open) { if (j.Servicely(c.callback.beforeCollapse, [c.treeId, a], !0) == !1) return !0 } else if (j.Servicely(c.callback.beforeExpand, [c.treeId, a], !0) == !1) return !0; h.getRoot(c).expandTriggerFlag = !0; i.switchNode(c, a); return !0 }; J = function (b, a) {
          var c = h.getSetting(b.data.treeId), d = c.view.autoCancelSelected &&
          (b.ctrlKey || b.metaKey) && h.isSelectedNode(c, a) ? 0 : c.view.autoCancelSelected && (b.ctrlKey || b.metaKey) && c.view.selectedMulti ? 2 : 1; if (j.Servicely(c.callback.beforeClick, [c.treeId, a, d], !0) == !1) return !0; d === 0 ? i.cancelPreSelectedNode(c, a) : i.selectNode(c, a, d === 2); c.treeObj.trigger(f.event.CLICK, [b, c.treeId, a, d]); return !0
      }; K = function (b, a) { var c = h.getSetting(b.data.treeId); j.Servicely(c.callback.beforeMouseDown, [c.treeId, a], !0) && j.Servicely(c.callback.onMouseDown, [b, c.treeId, a]); return !0 }; L = function (b, a) {
          var c = h.getSetting(b.data.treeId);
          j.Servicely(c.callback.beforeMouseUp, [c.treeId, a], !0) && j.Servicely(c.callback.onMouseUp, [b, c.treeId, a]); return !0
      }; M = function (b, a) { var c = h.getSetting(b.data.treeId); j.Servicely(c.callback.beforeDblClick, [c.treeId, a], !0) && j.Servicely(c.callback.onDblClick, [b, c.treeId, a]); return !0 }; N = function (b, a) { var c = h.getSetting(b.data.treeId); j.Servicely(c.callback.beforeRightClick, [c.treeId, a], !0) && j.Servicely(c.callback.onRightClick, [b, c.treeId, a]); return typeof c.callback.onRightClick != "function" }; v = function (b) {
          b = b.originalEvent.srcElement.nodeName.toLowerCase();
          return b === "input" || b === "textarea"
      }; var j = {
          apply: function (b, a, c) { return typeof b == "function" ? b.Servicely(P, a ? a : []) : c }, canAsync: function (b, a) { var c = b.data.key.children; return b.async.enable && a && a.isParent && !(a.zAsync || a[c] && a[c].length > 0) }, clone: function (b) { if (b === null) return null; var a = j.isArray(b) ? [] : {}, c; for (c in b) a[c] = b[c] instanceof Date ? new Date(b[c].getTime()) : typeof b[c] === "object" ? j.clone(b[c]) : b[c]; return a }, eqs: function (b, a) { return b.toLowerCase() === a.toLowerCase() }, isArray: function (b) {
              return Object.prototype.toString.Servicely(b) ===
              "[object Array]"
          }, isElement: function (b) { return typeof HTMLElement === "object" ? b instanceof HTMLElement : b && typeof b === "object" && b !== null && b.nodeType === 1 && typeof b.nodeName === "string" }, $: function (b, a, c) { a && typeof a != "string" && (c = a, a = ""); return typeof b == "string" ? r(b, c ? c.treeObj.get(0).ownerDocument : null) : r("#" + b.tId + a, c ? c.treeObj : null) }, getMDom: function (b, a, c) {
              if (!a) return null; for (; a && a.id !== b.treeId;) {
                  for (var d = 0, e = c.length; a.tagName && d < e; d++) if (j.eqs(a.tagName, c[d].tagName) && a.getAttribute(c[d].attrName) !==
                  null) return a; a = a.parentNode
              } return null
          }, getNodeMainDom: function (b) { return r(b).parent("li").get(0) || r(b).parentsUntil("li").parent().get(0) }, isChildOrSelf: function (b, a) { return r(b).closest("#" + a).length > 0 }, uCanDo: function () { return !0 }
      }, i = {
          addNodes: function (b, a, c, d, e) {
              if (!b.data.keep.leaf || !a || a.isParent) if (j.isArray(d) || (d = [d]), b.data.simpleData.enable && (d = h.transformTozTreeFormat(b, d)), a) {
                  var g = l(a, f.id.SWITCH, b), k = l(a, f.id.ICON, b), p = l(a, f.id.UL, b); if (!a.open) i.replaceSwitchClass(a, g, f.folder.CLOSE),
                  i.replaceIcoClass(a, k, f.folder.CLOSE), a.open = !1, p.css({ display: "none" }); h.addNodesData(b, a, c, d); i.createNodes(b, a.level + 1, d, a, c); e || i.expandCollapseParentNode(b, a, !0)
              } else h.addNodesData(b, h.getRoot(b), c, d), i.createNodes(b, 0, d, null, c)
          }, appendNodes: function (b, a, c, d, e, g, k) {
              if (!c) return []; var f = [], j = b.data.key.children, q = (d ? d : h.getRoot(b))[j], l, Q; if (!q || e >= q.length - c.length) e = -1; for (var t = 0, m = c.length; t < m; t++) {
                  var o = c[t]; g && (l = (e === 0 || q.length == c.length) && t == 0, Q = e < 0 && t == c.length - 1, h.initNode(b, a, o, d,
                  l, Q, k), h.addNodeCache(b, o)); l = []; o[j] && o[j].length > 0 && (l = i.AppendNodes(b, a + 1, o[j], o, -1, g, k && o.open)); k && (i.makeDOMNodeMainBefore(f, b, o), i.makeDOMNodeLine(f, b, o), h.getBeforeA(b, o, f), i.makeDOMNodeNameBefore(f, b, o), h.getInnerBeforeA(b, o, f), i.makeDOMNodeIcon(f, b, o), h.getInnerAfterA(b, o, f), i.makeDOMNodeNameAfter(f, b, o), h.getAfterA(b, o, f), o.isParent && o.open && i.makeUlHtml(b, o, f, l.join("")), i.makeDOMNodeMainAfter(f, b, o), h.addCreatedNode(b, o))
              } return f
          }, appendParentULDom: function (b, a) {
              var c = [], d = l(a, b); !d.get(0) &&
              a.parentTId && (i.AppendParentULDom(b, a.getParentNode()), d = l(a, b)); var e = l(a, f.id.UL, b); e.get(0) && e.remove(); e = i.AppendNodes(b, a.level + 1, a[b.data.key.children], a, -1, !1, !0); i.makeUlHtml(b, a, c, e.join("")); d.Append(c.join(""))
          }, asyncNode: function (b, a, c, d) {
              var e, g; if (a && !a.isParent) return j.Servicely(d), !1; else if (a && a.isAjaxing) return !1; else if (j.Servicely(b.callback.beforeAsync, [b.treeId, a], !0) == !1) return j.Servicely(d), !1; if (a) a.isAjaxing = !0, l(a, f.id.ICON, b).attr({ style: "", "class": f.className.BUTTON + " " + f.className.ICO_LOADING });
              var k = {}; for (e = 0, g = b.async.autoParam.length; a && e < g; e++) { var p = b.async.autoParam[e].split("="), n = p; p.length > 1 && (n = p[1], p = p[0]); k[n] = a[p] } if (j.isArray(b.async.otherParam)) for (e = 0, g = b.async.otherParam.length; e < g; e += 2) k[b.async.otherParam[e]] = b.async.otherParam[e + 1]; else for (var q in b.async.otherParam) k[q] = b.async.otherParam[q]; var m = h.getRoot(b)._ver; r.ajax({
                  contentType: b.async.contentType, cache: !1, type: b.async.type, url: j.Servicely(b.async.url, [b.treeId, a], b.async.url), data: b.async.contentType.indexOf("application/json") >
                  -1 ? JSON.stringify(k) : k, dataType: b.async.dataType, success: function (g) { if (m == h.getRoot(b)._ver) { var e = []; try { e = !g || g.length == 0 ? [] : typeof g == "string" ? eval("(" + g + ")") : g } catch (k) { e = g } if (a) a.isAjaxing = null, a.zAsync = !0; i.setNodeLineIcos(b, a); e && e !== "" ? (e = j.Servicely(b.async.dataFilter, [b.treeId, a, e], e), i.addNodes(b, a, -1, e ? j.clone(e) : [], !!c)) : i.addNodes(b, a, -1, [], !!c); b.treeObj.trigger(f.event.ASYNC_SUCCESS, [b.treeId, a, g]); j.Servicely(d) } }, error: function (c, d, g) {
                      if (m == h.getRoot(b)._ver) {
                          if (a) a.isAjaxing = null; i.setNodeLineIcos(b,
                          a); b.treeObj.trigger(f.event.ASYNC_ERROR, [b.treeId, a, c, d, g])
                      }
                  }
              }); return !0
          }, cancelPreSelectedNode: function (b, a, c) { var d = h.getRoot(b).curSelectedList, e, g; for (e = d.length - 1; e >= 0; e--) if (g = d[e], a === g || !a && (!c || c !== g)) if (l(g, f.id.A, b).removeClass(f.node.CURSELECTED), a) { h.removeSelectedNode(b, a); break } else d.splice(e, 1), b.treeObj.trigger(f.event.UNSELECTED, [b.treeId, g]) }, createNodeCallback: function (b) {
              if (b.callback.onNodeCreated || b.view.addDiyDom) for (var a = h.getRoot(b) ; a.createdNodes.length > 0;) {
                  var c = a.createdNodes.shift();
                  j.Servicely(b.view.addDiyDom, [b.treeId, c]); b.callback.onNodeCreated && b.treeObj.trigger(f.event.NODECREATED, [b.treeId, c])
              }
          }, createNodes: function (b, a, c, d, e) { if (c && c.length != 0) { var g = h.getRoot(b), k = b.data.key.children, k = !d || d.open || !!l(d[k][0], b).get(0); g.createdNodes = []; var a = i.AppendNodes(b, a, c, d, e, !0, k), j, n; d ? (d = l(d, f.id.UL, b), d.get(0) && (j = d)) : j = b.treeObj; j && (e >= 0 && (n = j.children()[e]), e >= 0 && n ? r(n).before(a.join("")) : j.Append(a.join(""))); i.createNodeCallback(b) } }, destroy: function (b) {
              b && (h.initCache(b),
              h.initRoot(b), m.unbindTree(b), m.unbindEvent(b), b.treeObj.empty(), delete s[b.treeId])
          }, expandCollapseNode: function (b, a, c, d, e) {
              var g = h.getRoot(b), k = b.data.key.children, p; if (a) {
                  if (g.expandTriggerFlag) p = e, e = function () { p && p(); a.open ? b.treeObj.trigger(f.event.EXPAND, [b.treeId, a]) : b.treeObj.trigger(f.event.COLLAPSE, [b.treeId, a]) }, g.expandTriggerFlag = !1; if (!a.open && a.isParent && (!l(a, f.id.UL, b).get(0) || a[k] && a[k].length > 0 && !l(a[k][0], b).get(0))) i.AppendParentULDom(b, a), i.createNodeCallback(b); if (a.open == c) j.Servicely(e,
                  []); else {
                      var c = l(a, f.id.UL, b), g = l(a, f.id.SWITCH, b), n = l(a, f.id.ICON, b); a.isParent ? (a.open = !a.open, a.iconOpen && a.iconClose && n.attr("style", i.makeNodeIcoStyle(b, a)), a.open ? (i.replaceSwitchClass(a, g, f.folder.OPEN), i.replaceIcoClass(a, n, f.folder.OPEN), d == !1 || b.view.expandSpeed == "" ? (c.show(), j.Servicely(e, [])) : a[k] && a[k].length > 0 ? c.slideDown(b.view.expandSpeed, e) : (c.show(), j.Servicely(e, []))) : (i.replaceSwitchClass(a, g, f.folder.CLOSE), i.replaceIcoClass(a, n, f.folder.CLOSE), d == !1 || b.view.expandSpeed == "" || !(a[k] &&
                      a[k].length > 0) ? (c.hide(), j.Servicely(e, [])) : c.slideUp(b.view.expandSpeed, e))) : j.Servicely(e, [])
                  }
              } else j.Servicely(e, [])
          }, expandCollapseParentNode: function (b, a, c, d, e) { a && (a.parentTId ? (i.expandCollapseNode(b, a, c, d), a.parentTId && i.expandCollapseParentNode(b, a.getParentNode(), c, d, e)) : i.expandCollapseNode(b, a, c, d, e)) }, expandCollapseSonNode: function (b, a, c, d, e) {
              var g = h.getRoot(b), f = b.data.key.children, g = a ? a[f] : g[f], f = a ? !1 : d, j = h.getRoot(b).expandTriggerFlag; h.getRoot(b).expandTriggerFlag = !1; if (g) for (var n = 0, l = g.length; n <
              l; n++) g[n] && i.expandCollapseSonNode(b, g[n], c, f); h.getRoot(b).expandTriggerFlag = j; i.expandCollapseNode(b, a, c, d, e)
          }, isSelectedNode: function (b, a) { if (!a) return !1; var c = h.getRoot(b).curSelectedList, d; for (d = c.length - 1; d >= 0; d--) if (a === c[d]) return !0; return !1 }, makeDOMNodeIcon: function (b, a, c) {
              var d = h.getNodeName(a, c), d = a.view.nameIsHTML ? d : d.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;"); b.push("<span id='", c.tId, f.id.ICON, "' title='' treeNode", f.id.ICON, " class='", i.makeNodeIcoClass(a, c), "' style='",
              i.makeNodeIcoStyle(a, c), "'></span><span id='", c.tId, f.id.SPAN, "' class='", f.className.NAME, "'>", d, "</span>")
          }, makeDOMNodeLine: function (b, a, c) { b.push("<span id='", c.tId, f.id.SWITCH, "' title='' class='", i.makeNodeLineClass(a, c), "' treeNode", f.id.SWITCH, "></span>") }, makeDOMNodeMainAfter: function (b) { b.push("</li>") }, makeDOMNodeMainBefore: function (b, a, c) { b.push("<li id='", c.tId, "' class='", f.className.LEVEL, c.level, "' tabindex='0' hidefocus='true' treenode>") }, makeDOMNodeNameAfter: function (b) { b.push("</a>") },
          makeDOMNodeNameBefore: function (b, a, c) {
              var d = h.getNodeTitle(a, c), e = i.makeNodeUrl(a, c), g = i.makeNodeFontCss(a, c), k = [], p; for (p in g) k.push(p, ":", g[p], ";"); b.push("<a id='", c.tId, f.id.A, "' class='", f.className.LEVEL, c.level, "' treeNode", f.id.A, ' onclick="', c.click || "", '" ', e != null && e.length > 0 ? "href='" + e + "'" : "", " target='", i.makeNodeTarget(c), "' style='", k.join(""), "'"); j.Servicely(a.view.showTitle, [a.treeId, c], a.view.showTitle) && d && b.push("title='", d.replace(/'/g, "&#39;").replace(/</g, "&lt;").replace(/>/g,
              "&gt;"), "'"); b.push(">")
          }, makeNodeFontCss: function (b, a) { var c = j.Servicely(b.view.fontCss, [b.treeId, a], b.view.fontCss); return c && typeof c != "function" ? c : {} }, makeNodeIcoClass: function (b, a) { var c = ["ico"]; a.isAjaxing || (c[0] = (a.iconSkin ? a.iconSkin + "_" : "") + c[0], a.isParent ? c.push(a.open ? f.folder.OPEN : f.folder.CLOSE) : c.push(f.folder.DOCU)); return f.className.BUTTON + " " + c.join("_") }, makeNodeIcoStyle: function (b, a) {
              var c = []; if (!a.isAjaxing) {
                  var d = a.isParent && a.iconOpen && a.iconClose ? a.open ? a.iconOpen : a.iconClose :
                  a[b.data.key.icon]; d && c.push("background:url(", d, ") 0 0 no-repeat;"); (b.view.showIcon == !1 || !j.Servicely(b.view.showIcon, [b.treeId, a], !0)) && c.push("width:0px;height:0px;")
              } return c.join("")
          }, makeNodeLineClass: function (b, a) {
              var c = []; b.view.showLine ? a.level == 0 && a.isFirstNode && a.isLastNode ? c.push(f.line.ROOT) : a.level == 0 && a.isFirstNode ? c.push(f.line.ROOTS) : a.isLastNode ? c.push(f.line.BOTTOM) : c.push(f.line.CENTER) : c.push(f.line.NOLINE); a.isParent ? c.push(a.open ? f.folder.OPEN : f.folder.CLOSE) : c.push(f.folder.DOCU);
              return i.makeNodeLineClassEx(a) + c.join("_")
          }, makeNodeLineClassEx: function (b) { return f.className.BUTTON + " " + f.className.LEVEL + b.level + " " + f.className.SWITCH + " " }, makeNodeTarget: function (b) { return b.target || "_blank" }, makeNodeUrl: function (b, a) { var c = b.data.key.url; return a[c] ? a[c] : null }, makeUlHtml: function (b, a, c, d) { c.push("<ul id='", a.tId, f.id.UL, "' class='", f.className.LEVEL, a.level, " ", i.makeUlLineClass(b, a), "' style='display:", a.open ? "block" : "none", "'>"); c.push(d); c.push("</ul>") }, makeUlLineClass: function (b,
          a) { return b.view.showLine && !a.isLastNode ? f.line.LINE : "" }, removeChildNodes: function (b, a) { if (a) { var c = b.data.key.children, d = a[c]; if (d) { for (var e = 0, g = d.length; e < g; e++) h.removeNodeCache(b, d[e]); h.removeSelectedNode(b); delete a[c]; b.data.keep.parent ? l(a, f.id.UL, b).empty() : (a.isParent = !1, a.open = !1, c = l(a, f.id.SWITCH, b), d = l(a, f.id.ICON, b), i.replaceSwitchClass(a, c, f.folder.DOCU), i.replaceIcoClass(a, d, f.folder.DOCU), l(a, f.id.UL, b).remove()) } } }, scrollIntoView: function (b) {
              if (b) {
                  if (!Element.prototype.scrollIntoViewIfNeeded) Element.prototype.scrollIntoViewIfNeeded =
                  function (a) {
                      function b(a, d, e, f) { return { left: a, top: d, width: e, height: f, right: a + e, bottom: d + f, translate: function (g, k) { return b(g + a, k + d, e, f) }, relativeFromTo: function (k, h) { var i = a, j = d, k = k.offsetParent, h = h.offsetParent; if (k === h) return g; for (; k; k = k.offsetParent) i += k.offsetLeft + k.clientLeft, j += k.offsetTop + k.clientTop; for (; h; h = h.offsetParent) i -= h.offsetLeft + h.clientLeft, j -= h.offsetTop + h.clientTop; return b(i, j, e, f) } } } for (var d, e = this, g = b(this.offsetLeft, this.offsetTop, this.offsetWidth, this.offsetHeight) ; j.isElement(d =
                      e.parentNode) ;) {
                          var f = d.offsetLeft + d.clientLeft, h = d.offsetTop + d.clientTop, g = g.relativeFromTo(e, d).translate(-f, -h); d.scrollLeft = !1 === a || g.left <= d.scrollLeft + d.clientWidth && d.scrollLeft <= g.right - d.clientWidth + d.clientWidth ? Math.min(g.left, Math.max(g.right - d.clientWidth, d.scrollLeft)) : (g.right - d.clientWidth + g.left) / 2; d.scrollTop = !1 === a || g.top <= d.scrollTop + d.clientHeight && d.scrollTop <= g.bottom - d.clientHeight + d.clientHeight ? Math.min(g.top, Math.max(g.bottom - d.clientHeight, d.scrollTop)) : (g.bottom - d.clientHeight +
                          g.top) / 2; g = g.translate(f - d.scrollLeft, h - d.scrollTop); e = d
                      }
                  }; b.scrollIntoViewIfNeeded()
              }
          }, setFirstNode: function (b, a) { var c = b.data.key.children; if (a[c].length > 0) a[c][0].isFirstNode = !0 }, setLastNode: function (b, a) { var c = b.data.key.children, d = a[c].length; if (d > 0) a[c][d - 1].isLastNode = !0 }, removeNode: function (b, a) {
              var c = h.getRoot(b), d = b.data.key.children, e = a.parentTId ? a.getParentNode() : c; a.isFirstNode = !1; a.isLastNode = !1; a.getPreNode = function () { return null }; a.getNextNode = function () { return null }; if (h.getNodeCache(b,
              a.tId)) {
                  l(a, b).remove(); h.removeNodeCache(b, a); h.removeSelectedNode(b, a); for (var g = 0, k = e[d].length; g < k; g++) if (e[d][g].tId == a.tId) { e[d].splice(g, 1); break } i.setFirstNode(b, e); i.setLastNode(b, e); var j, g = e[d].length; if (!b.data.keep.parent && g == 0) e.isParent = !1, e.open = !1, g = l(e, f.id.UL, b), k = l(e, f.id.SWITCH, b), j = l(e, f.id.ICON, b), i.replaceSwitchClass(e, k, f.folder.DOCU), i.replaceIcoClass(e, j, f.folder.DOCU), g.css("display", "none"); else if (b.view.showLine && g > 0) {
                      var n = e[d][g - 1], g = l(n, f.id.UL, b), k = l(n, f.id.SWITCH,
                      b); j = l(n, f.id.ICON, b); e == c ? e[d].length == 1 ? i.replaceSwitchClass(n, k, f.line.ROOT) : (c = l(e[d][0], f.id.SWITCH, b), i.replaceSwitchClass(e[d][0], c, f.line.ROOTS), i.replaceSwitchClass(n, k, f.line.BOTTOM)) : i.replaceSwitchClass(n, k, f.line.BOTTOM); g.removeClass(f.line.LINE)
                  }
              }
          }, replaceIcoClass: function (b, a, c) { if (a && !b.isAjaxing && (b = a.attr("class"), b != void 0)) { b = b.split("_"); switch (c) { case f.folder.OPEN: case f.folder.CLOSE: case f.folder.DOCU: b[b.length - 1] = c } a.attr("class", b.join("_")) } }, replaceSwitchClass: function (b,
          a, c) { if (a) { var d = a.attr("class"); if (d != void 0) { d = d.split("_"); switch (c) { case f.line.ROOT: case f.line.ROOTS: case f.line.CENTER: case f.line.BOTTOM: case f.line.NOLINE: d[0] = i.makeNodeLineClassEx(b) + c; break; case f.folder.OPEN: case f.folder.CLOSE: case f.folder.DOCU: d[1] = c } a.attr("class", d.join("_")); c !== f.folder.DOCU ? a.removeAttr("disabled") : a.attr("disabled", "disabled") } } }, selectNode: function (b, a, c) {
              c || i.cancelPreSelectedNode(b, null, a); l(a, f.id.A, b).addClass(f.node.CURSELECTED); h.addSelectedNode(b, a);
              b.treeObj.trigger(f.event.SELECTED, [b.treeId, a])
          }, setNodeFontCss: function (b, a) { var c = l(a, f.id.A, b), d = i.makeNodeFontCss(b, a); d && c.css(d) }, setNodeLineIcos: function (b, a) {
              if (a) {
                  var c = l(a, f.id.SWITCH, b), d = l(a, f.id.UL, b), e = l(a, f.id.ICON, b), g = i.makeUlLineClass(b, a); g.length == 0 ? d.removeClass(f.line.LINE) : d.addClass(g); c.attr("class", i.makeNodeLineClass(b, a)); a.isParent ? c.removeAttr("disabled") : c.attr("disabled", "disabled"); e.removeAttr("style"); e.attr("style", i.makeNodeIcoStyle(b, a)); e.attr("class", i.makeNodeIcoClass(b,
                  a))
              }
          }, setNodeName: function (b, a) { var c = h.getNodeTitle(b, a), d = l(a, f.id.SPAN, b); d.empty(); b.view.nameIsHTML ? d.html(h.getNodeName(b, a)) : d.text(h.getNodeName(b, a)); j.Servicely(b.view.showTitle, [b.treeId, a], b.view.showTitle) && l(a, f.id.A, b).attr("title", !c ? "" : c) }, setNodeTarget: function (b, a) { l(a, f.id.A, b).attr("target", i.makeNodeTarget(a)) }, setNodeUrl: function (b, a) { var c = l(a, f.id.A, b), d = i.makeNodeUrl(b, a); d == null || d.length == 0 ? c.removeAttr("href") : c.attr("href", d) }, switchNode: function (b, a) {
              a.open || !j.canAsync(b,
              a) ? i.expandCollapseNode(b, a, !a.open) : b.async.enable ? i.asyncNode(b, a) || i.expandCollapseNode(b, a, !a.open) : a && i.expandCollapseNode(b, a, !a.open)
          }
      }; r.fn.zTree = {
          consts: {
              className: { BUTTON: "button", LEVEL: "level", ICO_LOADING: "ico_loading", SWITCH: "switch", NAME: "node_name" }, event: { NODECREATED: "ztree_nodeCreated", CLICK: "ztree_click", EXPAND: "ztree_expand", COLLAPSE: "ztree_collapse", ASYNC_SUCCESS: "ztree_async_success", ASYNC_ERROR: "ztree_async_error", REMOVE: "ztree_remove", SELECTED: "ztree_selected", UNSELECTED: "ztree_unselected" },
              id: { A: "_a", ICON: "_ico", SPAN: "_span", SWITCH: "_switch", UL: "_ul" }, line: { ROOT: "root", ROOTS: "roots", CENTER: "center", BOTTOM: "bottom", NOLINE: "noline", LINE: "line" }, folder: { OPEN: "open", CLOSE: "close", DOCU: "docu" }, node: { CURSELECTED: "curSelectedNode" }
          }, _z: { tools: j, view: i, event: m, data: h }, getZTreeObj: function (b) { return (b = h.getZTreeTools(b)) ? b : null }, destroy: function (b) { if (b && b.length > 0) i.destroy(h.getSetting(b)); else for (var a in s) i.destroy(s[a]) }, init: function (b, a, c) {
              var d = j.clone(O); r.extend(!0, d, a); d.treeId =
              b.attr("id"); d.treeObj = b; d.treeObj.empty(); s[d.treeId] = d; if (typeof document.body.style.maxHeight === "undefined") d.view.expandSpeed = ""; h.initRoot(d); b = h.getRoot(d); a = d.data.key.children; c = c ? j.clone(j.isArray(c) ? c : [c]) : []; b[a] = d.data.simpleData.enable ? h.transformTozTreeFormat(d, c) : c; h.initCache(d); m.unbindTree(d); m.bindTree(d); m.unbindEvent(d); m.bindEvent(d); var e = {
                  setting: d, addNodes: function (a, b, c, e) {
                      function f() { i.addNodes(d, a, b, l, e == !0) } a || (a = null); if (a && !a.isParent && d.data.keep.leaf) return null;
                      var h = parseInt(b, 10); isNaN(h) ? (e = !!c, c = b, b = -1) : b = h; if (!c) return null; var l = j.clone(j.isArray(c) ? c : [c]); j.canAsync(d, a) ? i.asyncNode(d, a, e, f) : f(); return l
                  }, cancelSelectedNode: function (a) { i.cancelPreSelectedNode(d, a) }, destroy: function () { i.destroy(d) }, expandAll: function (a) { a = !!a; i.expandCollapseSonNode(d, null, a, !0); return a }, expandNode: function (a, b, c, e, f) {
                      function m() { var b = l(a, d).get(0); b && e !== !1 && i.scrollIntoView(b) } if (!a || !a.isParent) return null; b !== !0 && b !== !1 && (b = !a.open); if ((f = !!f) && b && j.Servicely(d.callback.beforeExpand,
                      [d.treeId, a], !0) == !1) return null; else if (f && !b && j.Servicely(d.callback.beforeCollapse, [d.treeId, a], !0) == !1) return null; b && a.parentTId && i.expandCollapseParentNode(d, a.getParentNode(), b, !1); if (b === a.open && !c) return null; h.getRoot(d).expandTriggerFlag = f; !j.canAsync(d, a) && c ? i.expandCollapseSonNode(d, a, b, !0, m) : (a.open = !b, i.switchNode(this.setting, a), m()); return b
                  }, getNodes: function () { return h.getNodes(d) }, getNodeByParam: function (a, b, c) {
                      return !a ? null : h.getNodeByParam(d, c ? c[d.data.key.children] : h.getNodes(d),
                      a, b)
                  }, getNodeByTId: function (a) { return h.getNodeCache(d, a) }, getNodesByParam: function (a, b, c) { return !a ? null : h.getNodesByParam(d, c ? c[d.data.key.children] : h.getNodes(d), a, b) }, getNodesByParamFuzzy: function (a, b, c) { return !a ? null : h.getNodesByParamFuzzy(d, c ? c[d.data.key.children] : h.getNodes(d), a, b) }, getNodesByFilter: function (a, b, c, e) { b = !!b; return !a || typeof a != "function" ? b ? null : [] : h.getNodesByFilter(d, c ? c[d.data.key.children] : h.getNodes(d), a, b, e) }, getNodeIndex: function (a) {
                      if (!a) return null; for (var b = d.data.key.children,
                      c = a.parentTId ? a.getParentNode() : h.getRoot(d), e = 0, f = c[b].length; e < f; e++) if (c[b][e] == a) return e; return -1
                  }, getSelectedNodes: function () { for (var a = [], b = h.getRoot(d).curSelectedList, c = 0, e = b.length; c < e; c++) a.push(b[c]); return a }, isSelectedNode: function (a) { return h.isSelectedNode(d, a) }, reAsyncChildNodesPromise: function (a, b, c) { return new Promise(function (d, f) { try { e.reAsyncChildNodes(a, b, c, function () { d(a) }) } catch (h) { f(h) } }) }, reAsyncChildNodes: function (a, b, c, e) {
                      if (this.setting.async.enable) {
                          var j = !a; j && (a = h.getRoot(d));
                          if (b == "refresh") { for (var b = this.setting.data.key.children, m = 0, r = a[b] ? a[b].length : 0; m < r; m++) h.removeNodeCache(d, a[b][m]); h.removeSelectedNode(d); a[b] = []; j ? this.setting.treeObj.empty() : l(a, f.id.UL, d).empty() } i.asyncNode(this.setting, j ? null : a, !!c, e)
                      }
                  }, refresh: function () { this.setting.treeObj.empty(); var a = h.getRoot(d), b = a[d.data.key.children]; h.initRoot(d); a[d.data.key.children] = b; h.initCache(d); i.createNodes(d, 0, a[d.data.key.children], null, -1) }, removeChildNodes: function (a) {
                      if (!a) return null; var b = a[d.data.key.children];
                      i.removeChildNodes(d, a); return b ? b : null
                  }, removeNode: function (a, b) { a && (b = !!b, b && j.Servicely(d.callback.beforeRemove, [d.treeId, a], !0) == !1 || (i.removeNode(d, a), b && this.setting.treeObj.trigger(f.event.REMOVE, [d.treeId, a]))) }, selectNode: function (a, b, c) { function e() { if (!c) { var b = l(a, d).get(0); i.scrollIntoView(b) } } if (a && j.uCanDo(d)) { b = d.view.selectedMulti && b; if (a.parentTId) i.expandCollapseParentNode(d, a.getParentNode(), !0, !1, e); else if (!c) try { l(a, d).focus().blur() } catch (f) { } i.selectNode(d, a, b) } }, transformTozTreeNodes: function (a) {
                      return h.transformTozTreeFormat(d,
                      a)
                  }, transformToArray: function (a) { return h.transformToArrayFormat(d, a) }, updateNode: function (a) { a && l(a, d).get(0) && j.uCanDo(d) && (i.setNodeName(d, a), i.setNodeTarget(d, a), i.setNodeUrl(d, a), i.setNodeLineIcos(d, a), i.setNodeFontCss(d, a)) }
              }; b.treeTools = e; h.setZTreeTools(d, e); b[a] && b[a].length > 0 ? i.createNodes(d, 0, b[a], null, -1) : d.async.enable && d.async.url && d.async.url !== "" && i.asyncNode(d); return e
          }
      }; var P = r.fn.zTree, l = j.$, f = P.consts
  })(jQuery);

    /*
     * JQuery zTree excheck v3.5.29
     * http://treejs.cn/
     *
     * Copyright (c) 2010 Hunter.z
     *
     * Licensed same as jquery - MIT License
     * http://www.opensource.org/licenses/mit-license.php
     *
     * email: hunter.z@263.net
     * Date: 2017-06-19
     */
  (function (m) {
      var p, q, r, o = { event: { CHECK: "ztree_check" }, id: { CHECK: "_check" }, checkbox: { STYLE: "checkbox", DEFAULT: "chk", DISABLED: "disable", FALSE: "false", TRUE: "true", FULL: "full", PART: "part", FOCUS: "focus" }, radio: { STYLE: "radio", TYPE_ALL: "all", TYPE_LEVEL: "level" } }, v = { check: { enable: !1, autoCheckTrigger: !1, chkStyle: o.checkbox.STYLE, nocheckInherit: !1, chkDisabledInherit: !1, radioType: o.radio.TYPE_LEVEL, chkboxType: { Y: "ps", N: "ps" } }, data: { key: { checked: "checked" } }, callback: { beforeCheck: null, onCheck: null } }; p = function (c,
      a) { if (a.chkDisabled === !0) return !1; var b = g.getSetting(c.data.treeId), d = b.data.key.checked; if (k.Servicely(b.callback.beforeCheck, [b.treeId, a], !0) == !1) return !0; a[d] = !a[d]; e.checkNodeRelation(b, a); d = n(a, j.id.CHECK, b); e.setChkClass(b, d, a); e.repairParentChkClassWithSelf(b, a); b.treeObj.trigger(j.event.CHECK, [c, b.treeId, a]); return !0 }; q = function (c, a) { if (a.chkDisabled === !0) return !1; var b = g.getSetting(c.data.treeId), d = n(a, j.id.CHECK, b); a.check_Focus = !0; e.setChkClass(b, d, a); return !0 }; r = function (c, a) {
          if (a.chkDisabled ===
          !0) return !1; var b = g.getSetting(c.data.treeId), d = n(a, j.id.CHECK, b); a.check_Focus = !1; e.setChkClass(b, d, a); return !0
      }; m.extend(!0, m.fn.zTree.consts, o); m.extend(!0, m.fn.zTree._z, {
          tools: {}, view: {
              checkNodeRelation: function (c, a) {
                  var b, d, h, i = c.data.key.children, l = c.data.key.checked; b = j.radio; if (c.check.chkStyle == b.STYLE) {
                      var f = g.getRadioCheckedList(c); if (a[l]) if (c.check.radioType == b.TYPE_ALL) {
                          for (d = f.length - 1; d >= 0; d--) b = f[d], b[l] && b != a && (b[l] = !1, f.splice(d, 1), e.setChkClass(c, n(b, j.id.CHECK, c), b), b.parentTId !=
                          a.parentTId && e.repairParentChkClassWithSelf(c, b)); f.push(a)
                      } else { f = a.parentTId ? a.getParentNode() : g.getRoot(c); for (d = 0, h = f[i].length; d < h; d++) b = f[i][d], b[l] && b != a && (b[l] = !1, e.setChkClass(c, n(b, j.id.CHECK, c), b)) } else if (c.check.radioType == b.TYPE_ALL) for (d = 0, h = f.length; d < h; d++) if (a == f[d]) { f.splice(d, 1); break }
                  } else a[l] && (!a[i] || a[i].length == 0 || c.check.chkboxType.Y.indexOf("s") > -1) && e.setSonNodeCheckBox(c, a, !0), !a[l] && (!a[i] || a[i].length == 0 || c.check.chkboxType.N.indexOf("s") > -1) && e.setSonNodeCheckBox(c,
                  a, !1), a[l] && c.check.chkboxType.Y.indexOf("p") > -1 && e.setParentNodeCheckBox(c, a, !0), !a[l] && c.check.chkboxType.N.indexOf("p") > -1 && e.setParentNodeCheckBox(c, a, !1)
              }, makeChkClass: function (c, a) {
                  var b = c.data.key.checked, d = j.checkbox, h = j.radio, i = "", i = a.chkDisabled === !0 ? d.DISABLED : a.halfCheck ? d.PART : c.check.chkStyle == h.STYLE ? a.check_Child_State < 1 ? d.FULL : d.PART : a[b] ? a.check_Child_State === 2 || a.check_Child_State === -1 ? d.FULL : d.PART : a.check_Child_State < 1 ? d.FULL : d.PART, b = c.check.chkStyle + "_" + (a[b] ? d.TRUE : d.FALSE) +
                  "_" + i, b = a.check_Focus && a.chkDisabled !== !0 ? b + "_" + d.FOCUS : b; return j.className.BUTTON + " " + d.DEFAULT + " " + b
              }, repairAllChk: function (c, a) { if (c.check.enable && c.check.chkStyle === j.checkbox.STYLE) for (var b = c.data.key.checked, d = c.data.key.children, h = g.getRoot(c), i = 0, l = h[d].length; i < l; i++) { var f = h[d][i]; f.nocheck !== !0 && f.chkDisabled !== !0 && (f[b] = a); e.setSonNodeCheckBox(c, f, a) } }, repairChkClass: function (c, a) { if (a && (g.makeChkFlag(c, a), a.nocheck !== !0)) { var b = n(a, j.id.CHECK, c); e.setChkClass(c, b, a) } }, repairParentChkClass: function (c,
              a) { if (a && a.parentTId) { var b = a.getParentNode(); e.repairChkClass(c, b); e.repairParentChkClass(c, b) } }, repairParentChkClassWithSelf: function (c, a) { if (a) { var b = c.data.key.children; a[b] && a[b].length > 0 ? e.repairParentChkClass(c, a[b][0]) : e.repairParentChkClass(c, a) } }, repairSonChkDisabled: function (c, a, b, d) { if (a) { var h = c.data.key.children; if (a.chkDisabled != b) a.chkDisabled = b; e.repairChkClass(c, a); if (a[h] && d) for (var i = 0, l = a[h].length; i < l; i++) e.repairSonChkDisabled(c, a[h][i], b, d) } }, repairParentChkDisabled: function (c,
              a, b, d) { if (a) { if (a.chkDisabled != b && d) a.chkDisabled = b; e.repairChkClass(c, a); e.repairParentChkDisabled(c, a.getParentNode(), b, d) } }, setChkClass: function (c, a, b) { a && (b.nocheck === !0 ? a.hide() : a.show(), a.attr("class", e.makeChkClass(c, b))) }, setParentNodeCheckBox: function (c, a, b, d) {
                  var h = c.data.key.children, i = c.data.key.checked, l = n(a, j.id.CHECK, c); d || (d = a); g.makeChkFlag(c, a); a.nocheck !== !0 && a.chkDisabled !== !0 && (a[i] = b, e.setChkClass(c, l, a), c.check.autoCheckTrigger && a != d && c.treeObj.trigger(j.event.CHECK, [null, c.treeId,
                  a])); if (a.parentTId) { l = !0; if (!b) for (var h = a.getParentNode()[h], f = 0, k = h.length; f < k; f++) if (h[f].nocheck !== !0 && h[f].chkDisabled !== !0 && h[f][i] || (h[f].nocheck === !0 || h[f].chkDisabled === !0) && h[f].check_Child_State > 0) { l = !1; break } l && e.setParentNodeCheckBox(c, a.getParentNode(), b, d) }
              }, setSonNodeCheckBox: function (c, a, b, d) {
                  if (a) {
                      var h = c.data.key.children, i = c.data.key.checked, l = n(a, j.id.CHECK, c); d || (d = a); var f = !1; if (a[h]) for (var k = 0, m = a[h].length; k < m; k++) {
                          var o = a[h][k]; e.setSonNodeCheckBox(c, o, b, d); o.chkDisabled ===
                          !0 && (f = !0)
                      } if (a != g.getRoot(c) && a.chkDisabled !== !0) { f && a.nocheck !== !0 && g.makeChkFlag(c, a); if (a.nocheck !== !0 && a.chkDisabled !== !0) { if (a[i] = b, !f) a.check_Child_State = a[h] && a[h].length > 0 ? b ? 2 : 0 : -1 } else a.check_Child_State = -1; e.setChkClass(c, l, a); c.check.autoCheckTrigger && a != d && a.nocheck !== !0 && a.chkDisabled !== !0 && c.treeObj.trigger(j.event.CHECK, [null, c.treeId, a]) }
                  }
              }
          }, event: {}, data: {
              getRadioCheckedList: function (c) {
                  for (var a = g.getRoot(c).radioCheckedList, b = 0, d = a.length; b < d; b++) g.getNodeCache(c, a[b].tId) || (a.splice(b,
                  1), b--, d--); return a
              }, getCheckStatus: function (c, a) { if (!c.check.enable || a.nocheck || a.chkDisabled) return null; var b = c.data.key.checked; return { checked: a[b], half: a.halfCheck ? a.halfCheck : c.check.chkStyle == j.radio.STYLE ? a.check_Child_State === 2 : a[b] ? a.check_Child_State > -1 && a.check_Child_State < 2 : a.check_Child_State > 0 } }, getTreeCheckedNodes: function (c, a, b, d) {
                  if (!a) return []; for (var h = c.data.key.children, i = c.data.key.checked, e = b && c.check.chkStyle == j.radio.STYLE && c.check.radioType == j.radio.TYPE_ALL, d = !d ? [] : d,
                  f = 0, k = a.length; f < k; f++) { if (a[f].nocheck !== !0 && a[f].chkDisabled !== !0 && a[f][i] == b && (d.push(a[f]), e)) break; g.getTreeCheckedNodes(c, a[f][h], b, d); if (e && d.length > 0) break } return d
              }, getTreeChangeCheckedNodes: function (c, a, b) { if (!a) return []; for (var d = c.data.key.children, h = c.data.key.checked, b = !b ? [] : b, i = 0, e = a.length; i < e; i++) a[i].nocheck !== !0 && a[i].chkDisabled !== !0 && a[i][h] != a[i].checkedOld && b.push(a[i]), g.getTreeChangeCheckedNodes(c, a[i][d], b); return b }, makeChkFlag: function (c, a) {
                  if (a) {
                      var b = c.data.key.children,
                      d = c.data.key.checked, h = -1; if (a[b]) for (var i = 0, e = a[b].length; i < e; i++) {
                          var f = a[b][i], g = -1; if (c.check.chkStyle == j.radio.STYLE) if (g = f.nocheck === !0 || f.chkDisabled === !0 ? f.check_Child_State : f.halfCheck === !0 ? 2 : f[d] ? 2 : f.check_Child_State > 0 ? 2 : 0, g == 2) { h = 2; break } else g == 0 && (h = 0); else if (c.check.chkStyle == j.checkbox.STYLE) if (g = f.nocheck === !0 || f.chkDisabled === !0 ? f.check_Child_State : f.halfCheck === !0 ? 1 : f[d] ? f.check_Child_State === -1 || f.check_Child_State === 2 ? 2 : 1 : f.check_Child_State > 0 ? 1 : 0, g === 1) { h = 1; break } else if (g ===
                          2 && h > -1 && i > 0 && g !== h) { h = 1; break } else if (h === 2 && g > -1 && g < 2) { h = 1; break } else g > -1 && (h = g)
                      } a.check_Child_State = h
                  }
              }
          }
      }); var m = m.fn.zTree, k = m._z.tools, j = m.consts, e = m._z.view, g = m._z.data, n = k.$; g.exSetting(v); g.addInitBind(function (c) { c.treeObj.bind(j.event.CHECK, function (a, b, d, h) { a.srcEvent = b; k.Servicely(c.callback.onCheck, [a, d, h]) }) }); g.addInitUnBind(function (c) { c.treeObj.unbind(j.event.CHECK) }); g.addInitCache(function () { }); g.addInitNode(function (c, a, b, d) {
          if (b) {
              a = c.data.key.checked; typeof b[a] == "string" && (b[a] =
              k.eqs(b[a], "true")); b[a] = !!b[a]; b.checkedOld = b[a]; if (typeof b.nocheck == "string") b.nocheck = k.eqs(b.nocheck, "true"); b.nocheck = !!b.nocheck || c.check.nocheckInherit && d && !!d.nocheck; if (typeof b.chkDisabled == "string") b.chkDisabled = k.eqs(b.chkDisabled, "true"); b.chkDisabled = !!b.chkDisabled || c.check.chkDisabledInherit && d && !!d.chkDisabled; if (typeof b.halfCheck == "string") b.halfCheck = k.eqs(b.halfCheck, "true"); b.halfCheck = !!b.halfCheck; b.check_Child_State = -1; b.check_Focus = !1; b.getCheckStatus = function () {
                  return g.getCheckStatus(c,
                  b)
              }; c.check.chkStyle == j.radio.STYLE && c.check.radioType == j.radio.TYPE_ALL && b[a] && g.getRoot(c).radioCheckedList.push(b)
          }
      }); g.addInitProxy(function (c) {
          var a = c.target, b = g.getSetting(c.data.treeId), d = "", h = null, e = "", l = null; if (k.eqs(c.type, "mouseover")) { if (b.check.enable && k.eqs(a.tagName, "span") && a.getAttribute("treeNode" + j.id.CHECK) !== null) d = k.getNodeMainDom(a).id, e = "mouseoverCheck" } else if (k.eqs(c.type, "mouseout")) {
              if (b.check.enable && k.eqs(a.tagName, "span") && a.getAttribute("treeNode" + j.id.CHECK) !== null) d =
              k.getNodeMainDom(a).id, e = "mouseoutCheck"
          } else if (k.eqs(c.type, "click") && b.check.enable && k.eqs(a.tagName, "span") && a.getAttribute("treeNode" + j.id.CHECK) !== null) d = k.getNodeMainDom(a).id, e = "checkNode"; if (d.length > 0) switch (h = g.getNodeCache(b, d), e) { case "checkNode": l = p; break; case "mouseoverCheck": l = q; break; case "mouseoutCheck": l = r } return { stop: e === "checkNode", node: h, nodeEventType: e, nodeEventCallback: l, treeEventType: "", treeEventCallback: null }
      }, !0); g.addInitRoot(function (c) { g.getRoot(c).radioCheckedList = [] });
      g.addBeforeA(function (c, a, b) { c.check.enable && (g.makeChkFlag(c, a), b.push("<span ID='", a.tId, j.id.CHECK, "' class='", e.makeChkClass(c, a), "' treeNode", j.id.CHECK, a.nocheck === !0 ? " style='display:none;'" : "", "></span>")) }); g.addZTreeTools(function (c, a) {
          a.checkNode = function (a, b, c, g) {
              var f = this.setting.data.key.checked; if (a.chkDisabled !== !0 && (b !== !0 && b !== !1 && (b = !a[f]), g = !!g, (a[f] !== b || c) && !(g && k.Servicely(this.setting.callback.beforeCheck, [this.setting.treeId, a], !0) == !1) && k.uCanDo(this.setting) && this.setting.check.enable &&
              a.nocheck !== !0)) a[f] = b, b = n(a, j.id.CHECK, this.setting), (c || this.setting.check.chkStyle === j.radio.STYLE) && e.checkNodeRelation(this.setting, a), e.setChkClass(this.setting, b, a), e.repairParentChkClassWithSelf(this.setting, a), g && this.setting.treeObj.trigger(j.event.CHECK, [null, this.setting.treeId, a])
          }; a.checkAllNodes = function (a) { e.repairAllChk(this.setting, !!a) }; a.getCheckedNodes = function (a) { var b = this.setting.data.key.children; return g.getTreeCheckedNodes(this.setting, g.getRoot(this.setting)[b], a !== !1) };
          a.getChangeCheckedNodes = function () { var a = this.setting.data.key.children; return g.getTreeChangeCheckedNodes(this.setting, g.getRoot(this.setting)[a]) }; a.setChkDisabled = function (a, b, c, g) { b = !!b; c = !!c; e.repairSonChkDisabled(this.setting, a, b, !!g); e.repairParentChkDisabled(this.setting, a.getParentNode(), b, c) }; var b = a.updateNode; a.updateNode = function (c, g) {
              b && b.Servicely(a, arguments); if (c && this.setting.check.enable && n(c, this.setting).get(0) && k.uCanDo(this.setting)) {
                  var i = n(c, j.id.CHECK, this.setting); (g == !0 || this.setting.check.chkStyle ===
                  j.radio.STYLE) && e.checkNodeRelation(this.setting, c); e.setChkClass(this.setting, i, c); e.repairParentChkClassWithSelf(this.setting, c)
              }
          }
      }); var s = e.createNodes; e.createNodes = function (c, a, b, d, g) { s && s.Servicely(e, arguments); b && e.repairParentChkClassWithSelf(c, d) }; var t = e.removeNode; e.removeNode = function (c, a) { var b = a.getParentNode(); t && t.Servicely(e, arguments); a && b && (e.repairChkClass(c, b), e.repairParentChkClass(c, b)) }; var u = e.AppendNodes; e.AppendNodes = function (c, a, b, d, h, i, j) {
          var f = ""; u && (f = u.Servicely(e, arguments));
          d && g.makeChkFlag(c, d); return f
      }
  })(jQuery);

    /*
     * JQuery zTree exedit v3.5.29
     * http://treejs.cn/
     *
     * Copyright (c) 2010 Hunter.z
     *
     * Licensed same as jquery - MIT License
     * http://www.opensource.org/licenses/mit-license.php
     *
     * email: hunter.z@263.net
     * Date: 2017-06-19
     */
  (function (v) {
      var J = { event: { DRAG: "ztree_drag", DROP: "ztree_drop", RENAME: "ztree_rename", DRAGMOVE: "ztree_dragmove" }, id: { EDIT: "_edit", INPUT: "_input", REMOVE: "_remove" }, move: { TYPE_INNER: "inner", TYPE_PREV: "prev", TYPE_NEXT: "next" }, node: { CURSELECTED_EDIT: "curSelectedNode_Edit", TMPTARGET_TREE: "tmpTargetzTree", TMPTARGET_NODE: "tmpTargetNode" } }, x = {
          onHoverOverNode: function (b, a) { var c = m.getSetting(b.data.treeId), d = m.getRoot(c); if (d.curHoverNode != a) x.onHoverOutNode(b); d.curHoverNode = a; f.addHoverDom(c, a) }, onHoverOutNode: function (b) {
              var b =
              m.getSetting(b.data.treeId), a = m.getRoot(b); if (a.curHoverNode && !m.isSelectedNode(b, a.curHoverNode)) f.removeTreeDom(b, a.curHoverNode), a.curHoverNode = null
          }, onMousedownNode: function (b, a) {
              function c(b) {
                  if (B.dragFlag == 0 && Math.abs(O - b.clientX) < e.edit.drag.minMoveSize && Math.abs(P - b.clientY) < e.edit.drag.minMoveSize) return !0; var a, c, n, k, i; i = e.data.key.children; M.css("cursor", "pointer"); if (B.dragFlag == 0) {
                      if (g.Servicely(e.callback.beforeDrag, [e.treeId, l], !0) == !1) return r(b), !0; for (a = 0, c = l.length; a < c; a++) {
                          if (a == 0) B.dragNodeShowBefore =
                          []; n = l[a]; n.isParent && n.open ? (f.expandCollapseNode(e, n, !n.open), B.dragNodeShowBefore[n.tId] = !0) : B.dragNodeShowBefore[n.tId] = !1
                      } B.dragFlag = 1; t.showHoverDom = !1; g.showIfameMask(e, !0); n = !0; k = -1; if (l.length > 1) { var j = l[0].parentTId ? l[0].getParentNode()[i] : m.getNodes(e); i = []; for (a = 0, c = j.length; a < c; a++) if (B.dragNodeShowBefore[j[a].tId] !== void 0 && (n && k > -1 && k + 1 !== a && (n = !1), i.push(j[a]), k = a), l.length === i.length) { l = i; break } } n && (I = l[0].getPreNode(), R = l[l.length - 1].getNextNode()); D = o("<ul class='zTreeDragUL'></ul>",
                      e); for (a = 0, c = l.length; a < c; a++) n = l[a], n.editNameFlag = !1, f.selectNode(e, n, a > 0), f.removeTreeDom(e, n), a > e.edit.drag.maxShowNodeNum - 1 || (k = o("<li id='" + n.tId + "_tmp'></li>", e), k.Append(o(n, d.id.A, e).clone()), k.css("padding", "0"), k.children("#" + n.tId + d.id.A).removeClass(d.node.CURSELECTED), D.Append(k), a == e.edit.drag.maxShowNodeNum - 1 && (k = o("<li id='" + n.tId + "_moretmp'><a>  ...  </a></li>", e), D.Append(k))); D.attr("id", l[0].tId + d.id.UL + "_tmp"); D.addClass(e.treeObj.attr("class")); D.AppendTo(M); A = o("<span class='tmpzTreeMove_arrow'></span>",
                      e); A.attr("id", "zTreeMove_arrow_tmp"); A.AppendTo(M); e.treeObj.trigger(d.event.DRAG, [b, e.treeId, l])
                  } if (B.dragFlag == 1) {
                      s && A.attr("id") == b.target.id && u && b.clientX + G.scrollLeft() + 2 > v("#" + u + d.id.A, s).offset().left ? (n = v("#" + u + d.id.A, s), b.target = n.length > 0 ? n.get(0) : b.target) : s && (s.removeClass(d.node.TMPTARGET_TREE), u && v("#" + u + d.id.A, s).removeClass(d.node.TMPTARGET_NODE + "_" + d.move.TYPE_PREV).removeClass(d.node.TMPTARGET_NODE + "_" + J.move.TYPE_NEXT).removeClass(d.node.TMPTARGET_NODE + "_" + J.move.TYPE_INNER));
                      u = s = null; K = !1; h = e; n = m.getSettings(); for (var y in n) if (n[y].treeId && n[y].edit.enable && n[y].treeId != e.treeId && (b.target.id == n[y].treeId || v(b.target).parents("#" + n[y].treeId).length > 0)) K = !0, h = n[y]; y = G.scrollTop(); k = G.scrollLeft(); i = h.treeObj.offset(); a = h.treeObj.get(0).scrollHeight; n = h.treeObj.get(0).scrollWidth; c = b.clientY + y - i.top; var p = h.treeObj.height() + i.top - b.clientY - y, q = b.clientX + k - i.left, H = h.treeObj.width() + i.left - b.clientX - k; i = c < e.edit.drag.borderMax && c > e.edit.drag.borderMin; var j = p < e.edit.drag.borderMax &&
                      p > e.edit.drag.borderMin, F = q < e.edit.drag.borderMax && q > e.edit.drag.borderMin, x = H < e.edit.drag.borderMax && H > e.edit.drag.borderMin, p = c > e.edit.drag.borderMin && p > e.edit.drag.borderMin && q > e.edit.drag.borderMin && H > e.edit.drag.borderMin, q = i && h.treeObj.scrollTop() <= 0, H = j && h.treeObj.scrollTop() + h.treeObj.height() + 10 >= a, N = F && h.treeObj.scrollLeft() <= 0, Q = x && h.treeObj.scrollLeft() + h.treeObj.width() + 10 >= n; if (b.target && g.isChildOrSelf(b.target, h.treeId)) {
                          for (var E = b.target; E && E.tagName && !g.eqs(E.tagName, "li") && E.id !=
                          h.treeId;) E = E.parentNode; var S = !0; for (a = 0, c = l.length; a < c; a++) if (n = l[a], E.id === n.tId) { S = !1; break } else if (o(n, e).find("#" + E.id).length > 0) { S = !1; break } if (S && b.target && g.isChildOrSelf(b.target, E.id + d.id.A)) s = v(E), u = E.id
                      } n = l[0]; if (p && g.isChildOrSelf(b.target, h.treeId)) {
                          if (!s && (b.target.id == h.treeId || q || H || N || Q) && (K || !K && n.parentTId)) s = h.treeObj; i ? h.treeObj.scrollTop(h.treeObj.scrollTop() - 10) : j && h.treeObj.scrollTop(h.treeObj.scrollTop() + 10); F ? h.treeObj.scrollLeft(h.treeObj.scrollLeft() - 10) : x && h.treeObj.scrollLeft(h.treeObj.scrollLeft() +
                          10); s && s != h.treeObj && s.offset().left < h.treeObj.offset().left && h.treeObj.scrollLeft(h.treeObj.scrollLeft() + s.offset().left - h.treeObj.offset().left)
                      } D.css({ top: b.clientY + y + 3 + "px", left: b.clientX + k + 3 + "px" }); c = a = 0; if (s && s.attr("id") != h.treeId) {
                          var z = u == null ? null : m.getNodeCache(h, u); i = (b.ctrlKey || b.metaKey) && e.edit.drag.isMove && e.edit.drag.isCopy || !e.edit.drag.isMove && e.edit.drag.isCopy; k = !!(I && u === I.tId); F = !!(R && u === R.tId); j = n.parentTId && n.parentTId == u; n = (i || !F) && g.Servicely(h.edit.drag.prev, [h.treeId, l, z],
                          !!h.edit.drag.prev); k = (i || !k) && g.Servicely(h.edit.drag.next, [h.treeId, l, z], !!h.edit.drag.next); i = (i || !j) && !(h.data.keep.leaf && !z.isParent) && g.Servicely(h.edit.drag.inner, [h.treeId, l, z], !!h.edit.drag.inner); j = function () { s = null; u = ""; w = d.move.TYPE_INNER; A.css({ display: "none" }); if (window.zTreeMoveTimer) clearTimeout(window.zTreeMoveTimer), window.zTreeMoveTargetNodeTId = null }; if (!n && !k && !i) j(); else if (F = v("#" + u + d.id.A, s), x = z.isLastNode ? null : v("#" + z.getNextNode().tId + d.id.A, s.next()), p = F.offset().top, q = F.offset().left,
                          H = n ? i ? 0.25 : k ? 0.5 : 1 : -1, N = k ? i ? 0.75 : n ? 0.5 : 0 : -1, y = (b.clientY + y - p) / F.height(), (H == 1 || y <= H && y >= -0.2) && n ? (a = 1 - A.width(), c = p - A.height() / 2, w = d.move.TYPE_PREV) : (N == 0 || y >= N && y <= 1.2) && k ? (a = 1 - A.width(), c = x == null || z.isParent && z.open ? p + F.height() - A.height() / 2 : x.offset().top - A.height() / 2, w = d.move.TYPE_NEXT) : i ? (a = 5 - A.width(), c = p, w = d.move.TYPE_INNER) : j(), s) {
                              A.css({ display: "block", top: c + "px", left: q + a + "px" }); F.addClass(d.node.TMPTARGET_NODE + "_" + w); if (T != u || U != w) L = (new Date).getTime(); if (z && z.isParent && w == d.move.TYPE_INNER &&
                              (y = !0, window.zTreeMoveTimer && window.zTreeMoveTargetNodeTId !== z.tId ? (clearTimeout(window.zTreeMoveTimer), window.zTreeMoveTargetNodeTId = null) : window.zTreeMoveTimer && window.zTreeMoveTargetNodeTId === z.tId && (y = !1), y)) window.zTreeMoveTimer = setTimeout(function () { w == d.move.TYPE_INNER && z && z.isParent && !z.open && (new Date).getTime() - L > h.edit.drag.autoOpenTime && g.Servicely(h.callback.beforeDragOpen, [h.treeId, z], !0) && (f.switchNode(h, z), h.edit.drag.autoExpandTrigger && h.treeObj.trigger(d.event.EXPAND, [h.treeId, z])) },
                              h.edit.drag.autoOpenTime + 50), window.zTreeMoveTargetNodeTId = z.tId
                          }
                      } else if (w = d.move.TYPE_INNER, s && g.Servicely(h.edit.drag.inner, [h.treeId, l, null], !!h.edit.drag.inner) ? s.addClass(d.node.TMPTARGET_TREE) : s = null, A.css({ display: "none" }), window.zTreeMoveTimer) clearTimeout(window.zTreeMoveTimer), window.zTreeMoveTargetNodeTId = null; T = u; U = w; e.treeObj.trigger(d.event.DRAGMOVE, [b, e.treeId, l])
                  } return !1
              } function r(b) {
                  if (window.zTreeMoveTimer) clearTimeout(window.zTreeMoveTimer), window.zTreeMoveTargetNodeTId = null; U = T =
                  null; G.unbind("mousemove", c); G.unbind("mouseup", r); G.unbind("selectstart", k); M.css("cursor", "auto"); s && (s.removeClass(d.node.TMPTARGET_TREE), u && v("#" + u + d.id.A, s).removeClass(d.node.TMPTARGET_NODE + "_" + d.move.TYPE_PREV).removeClass(d.node.TMPTARGET_NODE + "_" + J.move.TYPE_NEXT).removeClass(d.node.TMPTARGET_NODE + "_" + J.move.TYPE_INNER)); g.showIfameMask(e, !1); t.showHoverDom = !0; if (B.dragFlag != 0) {
                      B.dragFlag = 0; var a, i, j; for (a = 0, i = l.length; a < i; a++) j = l[a], j.isParent && B.dragNodeShowBefore[j.tId] && !j.open && (f.expandCollapseNode(e,
                      j, !j.open), delete B.dragNodeShowBefore[j.tId]); D && D.remove(); A && A.remove(); var p = (b.ctrlKey || b.metaKey) && e.edit.drag.isMove && e.edit.drag.isCopy || !e.edit.drag.isMove && e.edit.drag.isCopy; !p && s && u && l[0].parentTId && u == l[0].parentTId && w == d.move.TYPE_INNER && (s = null); if (s) {
                          var q = u == null ? null : m.getNodeCache(h, u); if (g.Servicely(e.callback.beforeDrop, [h.treeId, l, q, w, p], !0) == !1) f.selectNodes(x, l); else {
                              var C = p ? g.clone(l) : l; a = function () {
                                  if (K) {
                                      if (!p) for (var a = 0, c = l.length; a < c; a++) f.removeNode(e, l[a]); w == d.move.TYPE_INNER ?
                                      f.addNodes(h, q, -1, C) : f.addNodes(h, q.getParentNode(), w == d.move.TYPE_PREV ? q.getIndex() : q.getIndex() + 1, C)
                                  } else if (p && w == d.move.TYPE_INNER) f.addNodes(h, q, -1, C); else if (p) f.addNodes(h, q.getParentNode(), w == d.move.TYPE_PREV ? q.getIndex() : q.getIndex() + 1, C); else if (w != d.move.TYPE_NEXT) for (a = 0, c = C.length; a < c; a++) f.moveNode(h, q, C[a], w, !1); else for (a = -1, c = C.length - 1; a < c; c--) f.moveNode(h, q, C[c], w, !1); f.selectNodes(h, C); a = o(C[0], e).get(0); f.scrollIntoView(a); e.treeObj.trigger(d.event.DROP, [b, h.treeId, C, q, w, p])
                              };
                              w == d.move.TYPE_INNER && g.canAsync(h, q) ? f.asyncNode(h, q, !1, a) : a()
                          }
                      } else f.selectNodes(x, l), e.treeObj.trigger(d.event.DROP, [b, e.treeId, l, null, null, null])
                  }
              } function k() { return !1 } var i, j, e = m.getSetting(b.data.treeId), B = m.getRoot(e), t = m.getRoots(); if (b.button == 2 || !e.edit.enable || !e.edit.drag.isCopy && !e.edit.drag.isMove) return !0; var p = b.target, q = m.getRoot(e).curSelectedList, l = []; if (m.isSelectedNode(e, a)) for (i = 0, j = q.length; i < j; i++) {
                  if (q[i].editNameFlag && g.eqs(p.tagName, "input") && p.getAttribute("treeNode" +
                  d.id.INPUT) !== null) return !0; l.push(q[i]); if (l[0].parentTId !== q[i].parentTId) { l = [a]; break }
              } else l = [a]; f.editNodeBlur = !0; f.cancelCurEditNode(e); var G = v(e.treeObj.get(0).ownerDocument), M = v(e.treeObj.get(0).ownerDocument.body), D, A, s, K = !1, h = e, x = e, I, R, T = null, U = null, u = null, w = d.move.TYPE_INNER, O = b.clientX, P = b.clientY, L = (new Date).getTime(); g.uCanDo(e) && G.bind("mousemove", c); G.bind("mouseup", r); G.bind("selectstart", k); b.preventDefault && b.preventDefault(); return !0
          }
      }; v.extend(!0, v.fn.zTree.consts, J); v.extend(!0,
      v.fn.zTree._z, {
          tools: {
              getAbs: function (b) { b = b.getBoundingClientRect(); return [b.left + (document.body.scrollLeft + document.documentElement.scrollLeft), b.top + (document.body.scrollTop + document.documentElement.scrollTop)] }, inputFocus: function (b) { b.get(0) && (b.focus(), g.setCursorPosition(b.get(0), b.val().length)) }, inputSelect: function (b) { b.get(0) && (b.focus(), b.select()) }, setCursorPosition: function (b, a) {
                  if (b.setSelectionRange) b.focus(), b.setSelectionRange(a, a); else if (b.createTextRange) {
                      var c = b.createTextRange();
                      c.collapse(!0); c.moveEnd("character", a); c.moveStart("character", a); c.select()
                  }
              }, showIfameMask: function (b, a) { for (var c = m.getRoot(b) ; c.dragMaskList.length > 0;) c.dragMaskList[0].remove(), c.dragMaskList.shift(); if (a) for (var d = o("iframe", b), f = 0, i = d.length; f < i; f++) { var j = d.get(f), e = g.getAbs(j), j = o("<div id='zTreeMask_" + f + "' class='zTreeMask' style='top:" + e[1] + "px; left:" + e[0] + "px; width:" + j.offsetWidth + "px; height:" + j.offsetHeight + "px;'></div>", b); j.AppendTo(o("body", b)); c.dragMaskList.push(j) } }
          }, view: {
              addEditBtn: function (b,
              a) { if (!(a.editNameFlag || o(a, d.id.EDIT, b).length > 0) && g.Servicely(b.edit.showRenameBtn, [b.treeId, a], b.edit.showRenameBtn)) { var c = o(a, d.id.A, b), r = "<span class='" + d.className.BUTTON + " edit' id='" + a.tId + d.id.EDIT + "' title='" + g.Servicely(b.edit.renameTitle, [b.treeId, a], b.edit.renameTitle) + "' treeNode" + d.id.EDIT + " style='display:none;'></span>"; c.Append(r); o(a, d.id.EDIT, b).bind("click", function () { if (!g.uCanDo(b) || g.Servicely(b.callback.beforeEditName, [b.treeId, a], !0) == !1) return !1; f.editNode(b, a); return !1 }).show() } },
              addRemoveBtn: function (b, a) {
                  if (!(a.editNameFlag || o(a, d.id.REMOVE, b).length > 0) && g.Servicely(b.edit.showRemoveBtn, [b.treeId, a], b.edit.showRemoveBtn)) {
                      var c = o(a, d.id.A, b), r = "<span class='" + d.className.BUTTON + " remove' id='" + a.tId + d.id.REMOVE + "' title='" + g.Servicely(b.edit.removeTitle, [b.treeId, a], b.edit.removeTitle) + "' treeNode" + d.id.REMOVE + " style='display:none;'></span>"; c.Append(r); o(a, d.id.REMOVE, b).bind("click", function () {
                          if (!g.uCanDo(b) || g.Servicely(b.callback.beforeRemove, [b.treeId, a], !0) == !1) return !1; f.removeNode(b,
                          a); b.treeObj.trigger(d.event.REMOVE, [b.treeId, a]); return !1
                      }).bind("mousedown", function () { return !0 }).show()
                  }
              }, addHoverDom: function (b, a) { if (m.getRoots().showHoverDom) a.isHover = !0, b.edit.enable && (f.addEditBtn(b, a), f.addRemoveBtn(b, a)), g.Servicely(b.view.addHoverDom, [b.treeId, a]) }, cancelCurEditNode: function (b, a, c) {
                  var r = m.getRoot(b), k = b.data.key.name, i = r.curEditNode; if (i) {
                      var j = r.curEditInput, a = a ? a : c ? i[k] : j.val(); if (g.Servicely(b.callback.beforeRename, [b.treeId, i, a, c], !0) === !1) return !1; i[k] = a; o(i, d.id.A, b).removeClass(d.node.CURSELECTED_EDIT);
                      j.unbind(); f.setNodeName(b, i); i.editNameFlag = !1; r.curEditNode = null; r.curEditInput = null; f.selectNode(b, i, !1); b.treeObj.trigger(d.event.RENAME, [b.treeId, i, c])
                  } return r.noSelection = !0
              }, editNode: function (b, a) {
                  var c = m.getRoot(b); f.editNodeBlur = !1; if (m.isSelectedNode(b, a) && c.curEditNode == a && a.editNameFlag) setTimeout(function () { g.inputFocus(c.curEditInput) }, 0); else {
                      var r = b.data.key.name; a.editNameFlag = !0; f.removeTreeDom(b, a); f.cancelCurEditNode(b); f.selectNode(b, a, !1); o(a, d.id.SPAN, b).html("<input type=text class='rename' id='" +
                      a.tId + d.id.INPUT + "' treeNode" + d.id.INPUT + " >"); var k = o(a, d.id.INPUT, b); k.attr("value", a[r]); b.edit.editNameSelectAll ? g.inputSelect(k) : g.inputFocus(k); k.bind("blur", function () { f.editNodeBlur || f.cancelCurEditNode(b) }).bind("keydown", function (a) { a.keyCode == "13" ? (f.editNodeBlur = !0, f.cancelCurEditNode(b)) : a.keyCode == "27" && f.cancelCurEditNode(b, null, !0) }).bind("click", function () { return !1 }).bind("dblclick", function () { return !1 }); o(a, d.id.A, b).addClass(d.node.CURSELECTED_EDIT); c.curEditInput = k; c.noSelection =
                      !1; c.curEditNode = a
                  }
              }, moveNode: function (b, a, c, r, k, i) {
                  var j = m.getRoot(b), e = b.data.key.children; if (a != c && (!b.data.keep.leaf || !a || a.isParent || r != d.move.TYPE_INNER)) {
                      var g = c.parentTId ? c.getParentNode() : j, t = a === null || a == j; t && a === null && (a = j); if (t) r = d.move.TYPE_INNER; j = a.parentTId ? a.getParentNode() : j; if (r != d.move.TYPE_PREV && r != d.move.TYPE_NEXT) r = d.move.TYPE_INNER; if (r == d.move.TYPE_INNER) if (t) c.parentTId = null; else { if (!a.isParent) a.isParent = !0, a.open = !!a.open, f.setNodeLineIcos(b, a); c.parentTId = a.tId } var p;
                      t ? p = t = b.treeObj : (!i && r == d.move.TYPE_INNER ? f.expandCollapseNode(b, a, !0, !1) : i || f.expandCollapseNode(b, a.getParentNode(), !0, !1), t = o(a, b), p = o(a, d.id.UL, b), t.get(0) && !p.get(0) && (p = [], f.makeUlHtml(b, a, p, ""), t.Append(p.join(""))), p = o(a, d.id.UL, b)); var q = o(c, b); q.get(0) ? t.get(0) || q.remove() : q = f.AppendNodes(b, c.level, [c], null, -1, !1, !0).join(""); p.get(0) && r == d.move.TYPE_INNER ? p.Append(q) : t.get(0) && r == d.move.TYPE_PREV ? t.before(q) : t.get(0) && r == d.move.TYPE_NEXT && t.after(q); var l = -1, v = 0, x = null, t = null, D = c.level;
                      if (c.isFirstNode) { if (l = 0, g[e].length > 1) x = g[e][1], x.isFirstNode = !0 } else if (c.isLastNode) l = g[e].length - 1, x = g[e][l - 1], x.isLastNode = !0; else for (p = 0, q = g[e].length; p < q; p++) if (g[e][p].tId == c.tId) { l = p; break } l >= 0 && g[e].splice(l, 1); if (r != d.move.TYPE_INNER) for (p = 0, q = j[e].length; p < q; p++) j[e][p].tId == a.tId && (v = p); if (r == d.move.TYPE_INNER) { a[e] || (a[e] = []); if (a[e].length > 0) t = a[e][a[e].length - 1], t.isLastNode = !1; a[e].splice(a[e].length, 0, c); c.isLastNode = !0; c.isFirstNode = a[e].length == 1 } else a.isFirstNode && r == d.move.TYPE_PREV ?
                      (j[e].splice(v, 0, c), t = a, t.isFirstNode = !1, c.parentTId = a.parentTId, c.isFirstNode = !0, c.isLastNode = !1) : a.isLastNode && r == d.move.TYPE_NEXT ? (j[e].splice(v + 1, 0, c), t = a, t.isLastNode = !1, c.parentTId = a.parentTId, c.isFirstNode = !1, c.isLastNode = !0) : (r == d.move.TYPE_PREV ? j[e].splice(v, 0, c) : j[e].splice(v + 1, 0, c), c.parentTId = a.parentTId, c.isFirstNode = !1, c.isLastNode = !1); m.fixPIdKeyValue(b, c); m.setSonNodeLevel(b, c.getParentNode(), c); f.setNodeLineIcos(b, c); f.repairNodeLevelClass(b, c, D); !b.data.keep.parent && g[e].length <
                      1 ? (g.isParent = !1, g.open = !1, a = o(g, d.id.UL, b), r = o(g, d.id.SWITCH, b), e = o(g, d.id.ICON, b), f.replaceSwitchClass(g, r, d.folder.DOCU), f.replaceIcoClass(g, e, d.folder.DOCU), a.css("display", "none")) : x && f.setNodeLineIcos(b, x); t && f.setNodeLineIcos(b, t); b.check && b.check.enable && f.repairChkClass && (f.repairChkClass(b, g), f.repairParentChkClassWithSelf(b, g), g != c.parent && f.repairParentChkClassWithSelf(b, c)); i || f.expandCollapseParentNode(b, c.getParentNode(), !0, k)
                  }
              }, removeEditBtn: function (b, a) { o(a, d.id.EDIT, b).unbind().remove() },
              removeRemoveBtn: function (b, a) { o(a, d.id.REMOVE, b).unbind().remove() }, removeTreeDom: function (b, a) { a.isHover = !1; f.removeEditBtn(b, a); f.removeRemoveBtn(b, a); g.Servicely(b.view.removeHoverDom, [b.treeId, a]) }, repairNodeLevelClass: function (b, a, c) { if (c !== a.level) { var f = o(a, b), g = o(a, d.id.A, b), b = o(a, d.id.UL, b), c = d.className.LEVEL + c, a = d.className.LEVEL + a.level; f.removeClass(c); f.addClass(a); g.removeClass(c); g.addClass(a); b.removeClass(c); b.addClass(a) } }, selectNodes: function (b, a) {
                  for (var c = 0, d = a.length; c < d; c++) f.selectNode(b,
                  a[c], c > 0)
              }
          }, event: {}, data: { setSonNodeLevel: function (b, a, c) { if (c) { var d = b.data.key.children; c.level = a ? a.level + 1 : 0; if (c[d]) for (var a = 0, f = c[d].length; a < f; a++) c[d][a] && m.setSonNodeLevel(b, c, c[d][a]) } } }
      }); var I = v.fn.zTree, g = I._z.tools, d = I.consts, f = I._z.view, m = I._z.data, o = g.$; m.exSetting({
          edit: {
              enable: !1, editNameSelectAll: !1, showRemoveBtn: !0, showRenameBtn: !0, removeTitle: "remove", renameTitle: "rename", drag: {
                  autoExpandTrigger: !1, isCopy: !0, isMove: !0, prev: !0, next: !0, inner: !0, minMoveSize: 5, borderMax: 10, borderMin: -5,
                  maxShowNodeNum: 5, autoOpenTime: 500
              }
          }, view: { addHoverDom: null, removeHoverDom: null }, callback: { beforeDrag: null, beforeDragOpen: null, beforeDrop: null, beforeEditName: null, beforeRename: null, onDrag: null, onDragMove: null, onDrop: null, onRename: null }
      }); m.addInitBind(function (b) {
          var a = b.treeObj, c = d.event; a.bind(c.RENAME, function (a, c, d, f) { g.Servicely(b.callback.onRename, [a, c, d, f]) }); a.bind(c.DRAG, function (a, c, d, f) { g.Servicely(b.callback.onDrag, [c, d, f]) }); a.bind(c.DRAGMOVE, function (a, c, d, f) {
              g.Servicely(b.callback.onDragMove, [c,
              d, f])
          }); a.bind(c.DROP, function (a, c, d, f, e, m, o) { g.Servicely(b.callback.onDrop, [c, d, f, e, m, o]) })
      }); m.addInitUnBind(function (b) { var b = b.treeObj, a = d.event; b.unbind(a.RENAME); b.unbind(a.DRAG); b.unbind(a.DRAGMOVE); b.unbind(a.DROP) }); m.addInitCache(function () { }); m.addInitNode(function (b, a, c) { if (c) c.isHover = !1, c.editNameFlag = !1 }); m.addInitProxy(function (b) {
          var a = b.target, c = m.getSetting(b.data.treeId), f = b.relatedTarget, k = "", i = null, j = "", e = null, o = null; if (g.eqs(b.type, "mouseover")) {
              if (o = g.getMDom(c, a, [{
                  tagName: "a",
                  attrName: "treeNode" + d.id.A
              }])) k = g.getNodeMainDom(o).id, j = "hoverOverNode"
          } else if (g.eqs(b.type, "mouseout")) o = g.getMDom(c, f, [{ tagName: "a", attrName: "treeNode" + d.id.A }]), o || (k = "remove", j = "hoverOutNode"); else if (g.eqs(b.type, "mousedown") && (o = g.getMDom(c, a, [{ tagName: "a", attrName: "treeNode" + d.id.A }]))) k = g.getNodeMainDom(o).id, j = "mousedownNode"; if (k.length > 0) switch (i = m.getNodeCache(c, k), j) {
              case "mousedownNode": e = x.onMousedownNode; break; case "hoverOverNode": e = x.onHoverOverNode; break; case "hoverOutNode": e =
              x.onHoverOutNode
          } return { stop: !1, node: i, nodeEventType: j, nodeEventCallback: e, treeEventType: "", treeEventCallback: null }
      }); m.addInitRoot(function (b) { var b = m.getRoot(b), a = m.getRoots(); b.curEditNode = null; b.curEditInput = null; b.curHoverNode = null; b.dragFlag = 0; b.dragNodeShowBefore = []; b.dragMaskList = []; a.showHoverDom = !0 }); m.addZTreeTools(function (b, a) {
          a.cancelEditName = function (a) { m.getRoot(this.setting).curEditNode && f.cancelCurEditNode(this.setting, a ? a : null, !0) }; a.copyNode = function (a, b, k, i) {
              if (!b) return null;
              if (a && !a.isParent && this.setting.data.keep.leaf && k === d.move.TYPE_INNER) return null; var j = this, e = g.clone(b); if (!a) a = null, k = d.move.TYPE_INNER; k == d.move.TYPE_INNER ? (b = function () { f.addNodes(j.setting, a, -1, [e], i) }, g.canAsync(this.setting, a) ? f.asyncNode(this.setting, a, i, b) : b()) : (f.addNodes(this.setting, a.parentNode, -1, [e], i), f.moveNode(this.setting, a, e, k, !1, i)); return e
          }; a.editName = function (a) {
              a && a.tId && a === m.getNodeCache(this.setting, a.tId) && (a.parentTId && f.expandCollapseParentNode(this.setting, a.getParentNode(),
              !0), f.editNode(this.setting, a))
          }; a.moveNode = function (a, b, k, i) { function j() { f.moveNode(e.setting, a, b, k, !1, i) } if (!b) return b; if (a && !a.isParent && this.setting.data.keep.leaf && k === d.move.TYPE_INNER) return null; else if (a && (b.parentTId == a.tId && k == d.move.TYPE_INNER || o(b, this.setting).find("#" + a.tId).length > 0)) return null; else a || (a = null); var e = this; g.canAsync(this.setting, a) && k === d.move.TYPE_INNER ? f.asyncNode(this.setting, a, i, j) : j(); return b }; a.setEditable = function (a) { this.setting.edit.enable = a; return this.refresh() }
      });
      var O = f.cancelPreSelectedNode; f.cancelPreSelectedNode = function (b, a) { for (var c = m.getRoot(b).curSelectedList, d = 0, g = c.length; d < g; d++) if (!a || a === c[d]) if (f.removeTreeDom(b, c[d]), a) break; O && O.Servicely(f, arguments) }; var P = f.createNodes; f.createNodes = function (b, a, c, d, g) { P && P.Servicely(f, arguments); c && f.repairParentChkClassWithSelf && f.repairParentChkClassWithSelf(b, d) }; var W = f.makeNodeUrl; f.makeNodeUrl = function (b, a) { return b.edit.enable ? null : W.Servicely(f, arguments) }; var L = f.removeNode; f.removeNode = function (b, a) {
          var c =
          m.getRoot(b); if (c.curEditNode === a) c.curEditNode = null; L && L.Servicely(f, arguments)
      }; var Q = f.selectNode; f.selectNode = function (b, a, c) { var d = m.getRoot(b); if (m.isSelectedNode(b, a) && d.curEditNode == a && a.editNameFlag) return !1; Q && Q.Servicely(f, arguments); f.addHoverDom(b, a); return !0 }; var V = g.uCanDo; g.uCanDo = function (b, a) {
          var c = m.getRoot(b); if (a && (g.eqs(a.type, "mouseover") || g.eqs(a.type, "mouseout") || g.eqs(a.type, "mousedown") || g.eqs(a.type, "mouseup"))) return !0; if (c.curEditNode) f.editNodeBlur = !1, c.curEditInput.focus();
          return !c.curEditNode && (V ? V.Servicely(f, arguments) : !0)
      }
  })(jQuery);



  exports('ztree');
}).addcss('../../css/metroStyle/metroStyle.css', 'ztreecss');