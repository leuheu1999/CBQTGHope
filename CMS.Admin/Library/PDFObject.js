﻿/**
 *  PDFObject v2.2.7
 *  https://github.com/pipwerks/PDFObject
 *  @license
 *  Copyright (c) 2008-2021 Philip Hutchison
 *  MIT-style license: http://pipwerks.mit-license.org/
 *  UMD module pattern from https://github.com/umdjs/umd/blob/master/templates/returnExports.js
 */
!function (root, factory) { "function" == typeof define && define.amd ? define([], factory) : "object" == typeof module && module.exports ? module.exports = factory() : root.PDFObject = factory() }(this, function () { "use strict"; if ("undefined" == typeof window || void 0 === window.navigator || void 0 === window.navigator.userAgent || void 0 === window.navigator.mimeTypes) return !1; let nav = window.navigator, ua = window.navigator.userAgent, isIE = "ActiveXObject" in window, isModernBrowser = void 0 !== window.Promise, supportsPdfMimeType = void 0 !== nav.mimeTypes["application/pdf"], isMobileDevice = void 0 !== nav.platform && "MacIntel" === nav.platform && void 0 !== nav.maxTouchPoints && nav.maxTouchPoints > 1 || /Mobi|Tablet|Android|iPad|iPhone/.test(ua), isSafariDesktop = !isMobileDevice && void 0 !== nav.vendor && /Apple/.test(nav.vendor) && /Safari/.test(ua), isFirefoxWithPDFJS = !!(!isMobileDevice && /irefox/.test(ua) && ua.split("rv:").length > 1) && parseInt(ua.split("rv:")[1].split(".")[0], 10) > 18, createAXO = function (type) { var ax; try { ax = new ActiveXObject(type) } catch (e) { ax = null } return ax }, supportsPDFs = !isMobileDevice && (isModernBrowser || isFirefoxWithPDFJS || supportsPdfMimeType || isIE && !(!createAXO("AcroPDF.PDF") && !createAXO("PDF.PdfCtrl"))), embedError = function (msg, suppressConsole) { return suppressConsole || console.log("[PDFObject] " + msg), !1 }, emptyNodeContents = function (node) { for (; node.firstChild;)node.removeChild(node.firstChild) }, generatePDFJSMarkup = function (targetNode, url, pdfOpenFragment, PDFJS_URL, id, title, omitInlineStyles) { emptyNodeContents(targetNode); let fullURL = PDFJS_URL + "?file=" + encodeURIComponent(url) + pdfOpenFragment, div = document.createElement("div"), iframe = document.createElement("iframe"); return iframe.src = fullURL, iframe.className = "pdfobject", iframe.type = "application/pdf", iframe.frameborder = "0", iframe.allow = "fullscreen", iframe.title = title, id && (iframe.id = id), omitInlineStyles || (div.style.cssText = "position: absolute; top: 0; right: 0; bottom: 0; left: 0;", iframe.style.cssText = "border: none; width: 100%; height: 100%;", targetNode.style.position = "relative", targetNode.style.overflow = "auto"), div.appendChild(iframe), targetNode.appendChild(div), targetNode.classList.add("pdfobject-container"), targetNode.getElementsByTagName("iframe")[0] }, embed = function (url, targetSelector, options) { let selector = targetSelector || !1, opt = options || {}, id = "string" == typeof opt.id ? opt.id : "", page = opt.page || !1, pdfOpenParams = opt.pdfOpenParams || {}, fallbackLink = opt.fallbackLink || !0, width = opt.width || "100%", height = opt.height || "100%", title = opt.title || "Embedded PDF", assumptionMode = "boolean" != typeof opt.assumptionMode || opt.assumptionMode, forcePDFJS = "boolean" == typeof opt.forcePDFJS && opt.forcePDFJS, supportRedirect = "boolean" == typeof opt.supportRedirect && opt.supportRedirect, omitInlineStyles = "boolean" == typeof opt.omitInlineStyles && opt.omitInlineStyles, suppressConsole = "boolean" == typeof opt.suppressConsole && opt.suppressConsole, forceIframe = "boolean" == typeof opt.forceIframe && opt.forceIframe, PDFJS_URL = opt.PDFJS_URL || !1, targetNode = function (targetSelector) { let targetNode = document.body; return "string" == typeof targetSelector ? targetNode = document.querySelector(targetSelector) : void 0 !== window.jQuery && targetSelector instanceof jQuery && targetSelector.length ? targetNode = targetSelector.get(0) : void 0 !== targetSelector.nodeType && 1 === targetSelector.nodeType && (targetNode = targetSelector), targetNode }(selector), fallbackHTML = "", pdfOpenFragment = ""; if ("string" != typeof url) return embedError("URL is not valid", suppressConsole); if (!targetNode) return embedError("Target element cannot be determined", suppressConsole); if (page && (pdfOpenParams.page = page), pdfOpenFragment = function (pdfParams) { let prop, string = ""; if (pdfParams) { for (prop in pdfParams) pdfParams.hasOwnProperty(prop) && (string += encodeURIComponent(prop) + "=" + encodeURIComponent(pdfParams[prop]) + "&"); string && (string = (string = "#" + string).slice(0, string.length - 1)) } return string }(pdfOpenParams), forcePDFJS && PDFJS_URL) return generatePDFJSMarkup(targetNode, url, pdfOpenFragment, PDFJS_URL, id, title, omitInlineStyles); if (supportsPDFs || assumptionMode && !isMobileDevice) { return function (embedType, targetNode, targetSelector, url, pdfOpenFragment, width, height, id, title, omitInlineStyles) { emptyNodeContents(targetNode); let embed = document.createElement(embedType); if (embed.src = url + pdfOpenFragment, embed.className = "pdfobject", embed.type = "application/pdf", embed.title = title, id && (embed.id = id), "iframe" === embedType && (embed.allow = "fullscreen"), !omitInlineStyles) { let style = "embed" === embedType ? "overflow: auto;" : "border: none;"; targetSelector && targetSelector !== document.body ? style += "width: " + width + "; height: " + height + ";" : style += "position: absolute; top: 0; right: 0; bottom: 0; left: 0; width: 100%; height: 100%;", embed.style.cssText = style } return targetNode.classList.add("pdfobject-container"), targetNode.appendChild(embed), targetNode.getElementsByTagName(embedType)[0] }(forceIframe || supportRedirect || isSafariDesktop ? "iframe" : "embed", targetNode, targetSelector, url, pdfOpenFragment, width, height, id, title, omitInlineStyles) } return PDFJS_URL ? generatePDFJSMarkup(targetNode, url, pdfOpenFragment, PDFJS_URL, id, title, omitInlineStyles) : (fallbackLink && (fallbackHTML = "string" == typeof fallbackLink ? fallbackLink : "<p>This browser does not support inline PDFs. Please download the PDF to view it: <a href='[url]'>Download PDF</a></p>", targetNode.innerHTML = fallbackHTML.replace(/\[url\]/g, url)), embedError("This browser does not support embedded PDFs", suppressConsole)) }; return { embed: function (a, b, c) { return embed(a, b, c) }, pdfobjectversion: "2.2.7", supportsPDFs: supportsPDFs } });